import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { MsalService } from '@azure/msal-angular';
import { Observable, from, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class MsalAuthFacade {
  private readonly msalService = inject(MsalService);
  private readonly platformId = inject(PLATFORM_ID);

  acquireToken(scopes: string[]): Observable<string> {
    if (!isPlatformBrowser(this.platformId)) {
      return throwError(() => new Error('MSAL not available on server'));
    }
    const account = this.msalService.instance.getActiveAccount();
    if (!account) {
      return throwError(() => new Error('No active MSAL account'));
    }
    return from(
      this.msalService.instance.acquireTokenSilent({ scopes, account })
    ).pipe(
      map((result) => result.accessToken),
      catchError((err) => throwError(() => err))
    );
  }

  logout(): Observable<void> {
    if (!isPlatformBrowser(this.platformId)) {
      return throwError(() => new Error('MSAL not available on server'));
    }
    return from(this.msalService.instance.logoutRedirect()).pipe(
      catchError((err) => throwError(() => err))
    );
  }
}
