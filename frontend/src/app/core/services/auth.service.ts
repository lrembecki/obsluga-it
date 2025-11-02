import { Injectable, computed, inject } from '@angular/core';
import { Router } from '@angular/router';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { AccountInfo } from '@azure/msal-browser';
import { environment } from 'environments/environment';
import { lastValueFrom, switchMap } from 'rxjs';
import { PermissionType } from '../defaults/permission.default';
import { AccountModel } from '../models/account.model';
import { ServiceCallResult } from '../models/service-call-result.model';
import { ApiService } from './api.service';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly _msalService = inject(MsalService);
  private readonly _msalBroadcast = inject(MsalBroadcastService);
  private readonly _storage = inject(StorageService);
  private readonly _api = inject(ApiService);
  private readonly _router = inject(Router);

  public readonly isAuthenticated = computed(
    () =>
      (!!this._storage.account.data() &&
        new Date(this._storage.account.data()!.expires) > new Date() &&
        this._storage.account.data()?.isAuthenticated) ??
      false,
  );

  public isInRole(permission: PermissionType[]): boolean {
    return (
      this.isAuthenticated() &&
      (!permission.length ||
        permission.some((e) =>
          this._storage.account.data()?.permissions.includes(e),
        ))
    );
  }

  public async initialize(): Promise<void> {
    await lastValueFrom(this._msalService.initialize());

    this._msalService.handleRedirectObservable().subscribe();

    this._msalBroadcast.inProgress$.subscribe((progress) => {
      if (progress === 'none') {
        this.status();
      }
    });
  }

  public async authenticate(): Promise<boolean> {
    const accounts = this._msalService.instance.getAllAccounts();
    if (
      accounts.length &&
      (await this.authenticateInternal(accounts[0])).success
    ) {
      return true;
    }

    if (accounts.length) await this.logout();

    this._msalService
      .initialize()
      .pipe(
        switchMap(() =>
          this._msalService.loginRedirect({
            scopes: environment.msal.scopes.default,
          }),
        ),
      )
      .subscribe();

    return false;
  }

  private async authenticateInternal(
    account: AccountInfo,
  ): Promise<ServiceCallResult<AccountModel>> {
    await this.acquireAPIAccess(account);

    const response = await this._api.get<AccountModel>(`authenticate`);
    if (response.success) {
      this._storage.account.set(response.data!);
    }

    return response;
  }

  public async changeSubscription(subscriptionId: string): Promise<boolean> {
    const response = await this._api.get<AccountModel>(
      `authenticate/${subscriptionId}`,
    );
    if (response.success) this._storage.account.set(response.data!);

    return response.success;
  }

  public async logout(): Promise<void> {
    this._storage.b2cToken.set(null!);
    this._storage.account.set(null!);

    this._msalService.logoutRedirect();
  }

  public async status(): Promise<void> {
    const msalAccounts = this._msalService.instance.getAllAccounts();
    const isAuthenticated = this.isAuthenticated();

    if (msalAccounts.length && !isAuthenticated) {
      const response = await this.authenticateInternal(msalAccounts[0]);
      if (!response.success) {
        this._storage.account.set(
          new AccountModel({
            user: {
              email: ``,
              id: null!,
            },
          }),
        );

        this._router.navigate(['/account/unauthorized']);
      }
    }
  }

  private async acquireAPIAccess(account: AccountInfo): Promise<void> {
    try {
      let tokenResponse = await lastValueFrom(
        this._msalService.acquireTokenSilent({
          scopes: environment.msal.scopes.default,
          account,
        }),
      );

      if (!tokenResponse.accessToken) {
        // Make an explicit request for API access
        tokenResponse = await lastValueFrom(
          this._msalService.acquireTokenSilent({
            scopes: [
              'https://platformproduction.onmicrosoft.com/platform/.default',
            ],
            account,
          }),
        );
      }

      if (tokenResponse?.accessToken) {
        this._storage.b2cToken.set(tokenResponse.accessToken);
      } else {
        throw new Error(`Unable to acquire token`);
      }
    } catch (error) {
      console.error('Failed to acquire API access token:', error);
    }
  }
}
