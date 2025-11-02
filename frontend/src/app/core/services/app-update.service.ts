import { Injectable, inject, isDevMode } from '@angular/core';
import { SwUpdate, VersionReadyEvent } from '@angular/service-worker';
import { filter, fromEvent, merge } from 'rxjs';
import { NotificationInput, NotificationService } from './notification.service';
import { TranslationService } from './translation.service';

@Injectable({
  providedIn: 'root',
})
export class AppUpdateService {
  private readonly _swUpdate = inject(SwUpdate);
  private readonly _notification = inject(NotificationService);
  private readonly _translation = inject(TranslationService);

  constructor() {
    if (!isDevMode() && this._swUpdate.isEnabled) {
      this.initializeUpdateChecks();
    }
  }

  private initializeUpdateChecks(): void {
    // Check for updates immediately and then every 15 seconds for more responsive updates
    this.checkForUpdates();
    setInterval(() => this.checkForUpdates(), 15000);

    // Listen for version ready events
    this._swUpdate.versionUpdates
      .pipe(
        filter((evt): evt is VersionReadyEvent => evt.type === 'VERSION_READY'),
      )
      .subscribe(() => {
        this.showUpdateNotification();
      });

    // Listen for unrecoverable state
    this._swUpdate.unrecoverable.subscribe(() => {
      this.showReloadNotification();
    });

    // Listen for online/offline events to check for updates when coming back online
    merge(
      fromEvent(window, 'online'),
      fromEvent(window, 'focus'),
      fromEvent(window, 'visibilitychange'),
    ).subscribe(() => {
      if (!document.hidden) {
        this.checkForUpdates();
      }
    });
  }

  private async checkForUpdates(): Promise<void> {
    try {
      const hasUpdate = await this._swUpdate.checkForUpdate();
      if (hasUpdate) {
        console.log('New version available');
      }
    } catch (error) {
      console.error('Failed to check for updates:', error);
    }
  }

  private showUpdateNotification(): void {
    const title = this._translation.instant('UPDATE.NEW_VERSION_TITLE');
    const message = this._translation.instant('UPDATE.NEW_VERSION_MESSAGE');
    const confirmText = this._translation.instant('UI.OK') || 'OK';

    // Use browser's native confirm dialog for immediate user attention
    const confirmed = window.confirm(
      `${title}\n\n${message}\n\n${confirmText}?`,
    );

    if (confirmed) {
      this.activateUpdate();
    }
  }

  private showReloadNotification(): void {
    const title = this._translation.instant('UPDATE.RELOAD_REQUIRED_TITLE');
    const message = this._translation.instant('UPDATE.RELOAD_REQUIRED_MESSAGE');
    const confirmText = this._translation.instant('UI.OK') || 'OK';

    // Use browser's native confirm dialog for immediate user attention
    const confirmed = window.confirm(
      `${title}\n\n${message}\n\n${confirmText}?`,
    );

    if (confirmed) {
      this.reloadApp();
    }
  }

  private async activateUpdate(): Promise<void> {
    try {
      await this._swUpdate.activateUpdate();
      this.reloadApp();
    } catch (error) {
      console.error('Failed to activate update:', error);
      this.reloadApp(); // Fallback to reload even if activation fails
    }
  }

  private reloadApp(): void {
    // Clear any cached data before reload
    if ('caches' in window) {
      caches.keys().then((names) => {
        names.forEach((name) => {
          caches.delete(name);
        });
      });
    }

    // Force reload the page
    window.location.reload();
  }

  /**
   * Manually trigger an update check
   */
  public async manualUpdateCheck(): Promise<void> {
    if (!this._swUpdate.isEnabled) {
      console.warn('Service Worker is not enabled');
      return;
    }

    try {
      const hasUpdate = await this._swUpdate.checkForUpdate();
      if (!hasUpdate) {
        this._notification.success(
          new NotificationInput({
            title: this._translation.instant('UPDATE.NO_UPDATE_TITLE'),
            message: this._translation.instant('UPDATE.NO_UPDATE_MESSAGE'),
          }),
        );
      }
    } catch (error) {
      console.error('Manual update check failed:', error);
      this._notification.error(
        new NotificationInput({
          title: this._translation.instant('ERRORS.SOMETHING_WENT_WRONG'),
          message: this._translation.instant('UPDATE.UPDATE_CHECK_FAILED'),
        }),
      );
    }
  }
}
