import { inject, Injectable } from '@angular/core';
import { AuthFacade } from '../facades/auth.facade';

const ROTATION_THRESHOLD_MS = 5 * 60 * 1000; // 5 minutes before expiry
const MAX_ROTATION_FAILURES = 3;
const RETRY_DELAY_MS = 60_000; // 1 minute retry delay on failure

@Injectable({ providedIn: 'root' })
export class TokenRotationService {
  private readonly authFacade = inject(AuthFacade);
  private rotationTimer: ReturnType<typeof setTimeout> | null = null;
  private failureCount = 0;

  startRotation(): void {
    this.scheduleRotation();
  }

  stopRotation(): void {
    if (this.rotationTimer) {
      clearTimeout(this.rotationTimer);
      this.rotationTimer = null;
    }
  }

  private scheduleRotation(): void {
    const session = this.authFacade.session();
    if (!session) return;

    const now = Date.now();
    const delay = session.expiresAt - now - ROTATION_THRESHOLD_MS;

    if (delay <= 0) {
      this.rotate();
      return;
    }

    this.rotationTimer = setTimeout(() => this.rotate(), delay);
  }

  private rotate(): void {
    this.authFacade.authenticate().subscribe({
      next: () => {
        this.failureCount = 0;
        this.scheduleRotation();
      },
      error: () => {
        this.failureCount++;
        if (this.failureCount >= MAX_ROTATION_FAILURES) {
          this.authFacade.logout().subscribe();
        } else {
          setTimeout(() => this.rotate(), RETRY_DELAY_MS);
        }
      },
    });
  }
}
