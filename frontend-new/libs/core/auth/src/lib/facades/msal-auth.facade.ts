import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { MsalService } from '@azure/msal-angular';
import { AccountInfo } from '@azure/msal-browser';
import {
  EMPTY,
  Observable,
  firstValueFrom,
  from,
  throwError,
  switchMap,
} from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class MsalAuthFacade {
  private readonly msalService = inject(MsalService);
  private readonly platformId = inject(PLATFORM_ID);
  private msalInitializationPromise: Promise<void> | null = null;

  private restoreActiveAccount(): void {
    if (this.msalService.instance.getActiveAccount()) {
      return;
    }

    const cachedAccount = this.msalService.instance.getAllAccounts()[0] ?? null;
    if (cachedAccount) {
      this.msalService.instance.setActiveAccount(cachedAccount);
    }
  }

  private defaultScopes(scopes: string[]): string[] {
    const firstScope = scopes.find((scope) =>
      scope.startsWith('https://') || scope.startsWith('http://')
    );

    if (!firstScope) {
      return [];
    }

    if (firstScope.endsWith('/.default')) {
      return [firstScope];
    }

    const separatorIndex = firstScope.lastIndexOf('/');
    if (separatorIndex <= 'https://'.length) {
      return [];
    }

    return [`${firstScope.substring(0, separatorIndex)}/.default`];
  }

  private acquireTokenSilent(
    account: AccountInfo,
    scopes: string[]
  ): Observable<string> {
    return from(
      this.msalService.instance.acquireTokenSilent({ scopes, account })
    ).pipe(map((result) => result.accessToken));
  }

  private acquireTokenRedirect(scopes: string[]): Observable<string> {
    return this.msalService
      .acquireTokenRedirect({ scopes })
      .pipe(switchMap(() => EMPTY));
  }

  private ensureInitialized(): Observable<void> {
    this.msalInitializationPromise ??= firstValueFrom(
      this.msalService.initialize()
    ).then(() => undefined);

    return from(this.msalInitializationPromise);
  }

  private ensureActiveAccount(scopes: string[]): Observable<void> {
    return this.ensureInitialized().pipe(
      switchMap(() => {
        this.restoreActiveAccount();
        const activeAccount = this.msalService.instance.getActiveAccount();
        if (activeAccount) {
          return from(Promise.resolve());
        }

        return this.msalService
          .loginRedirect({ scopes })
          .pipe(switchMap(() => EMPTY));
      })
    );
  }

  acquireToken(scopes: string[]): Observable<string> {
    if (!isPlatformBrowser(this.platformId)) {
      return throwError(() => new Error('MSAL not available on server'));
    }

    return this.ensureActiveAccount(scopes).pipe(
      switchMap(() => {
        this.restoreActiveAccount();
        const account = this.msalService.instance.getActiveAccount();
        if (!account) {
          return throwError(() => new Error('No active MSAL account'));
        }

        return this.acquireTokenSilent(account, scopes).pipe(
          catchError(() => {
            const defaultScopes = this.defaultScopes(scopes);

            if (defaultScopes.length === 0) {
              return this.acquireTokenRedirect(scopes);
            }

            return this.acquireTokenSilent(account, defaultScopes).pipe(
              catchError(() => this.acquireTokenRedirect(scopes))
            );
          })
        );
      }),
      catchError((err) => throwError(() => err))
    );
  }

  logout(): Observable<void> {
    if (!isPlatformBrowser(this.platformId)) {
      return throwError(() => new Error('MSAL not available on server'));
    }
    return this.ensureInitialized().pipe(
      switchMap(() => from(this.msalService.instance.logoutRedirect())),
      catchError((err) => throwError(() => err))
    );
  }
}
