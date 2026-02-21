import { inject, Injectable, signal } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map, catchError, throwError, switchMap } from 'rxjs';
import { MsalAuthFacade } from './msal-auth.facade';
import { JwtStorageService } from '../services/jwt-storage.service';
import { AuthSession } from '../models/auth-session.model';

const AUTH_ENDPOINT = 'api-endpoint/account/authenticate';
const MSAL_SCOPES = ['User.Read'];

@Injectable({ providedIn: 'root' })
export class AuthFacade {
  private readonly http = inject(HttpClient);
  private readonly msalFacade = inject(MsalAuthFacade);
  private readonly jwtStorage = inject(JwtStorageService);

  readonly session = signal<AuthSession | null>(null);

  initialize(): void {
    const stored = this.jwtStorage.retrieve();
    if (stored) {
      this.session.set(stored);
    }
  }

  authenticate(): Observable<AuthSession> {
    return this.msalFacade.acquireToken(MSAL_SCOPES).pipe(
      switchMap((msalToken) => {
        const headers = new HttpHeaders({ Authorization: `Bearer ${msalToken}` });
        return this.http.post<{ token: string; expiresAt: number }>(
          AUTH_ENDPOINT,
          {},
          { headers }
        );
      }),
      map((response) => {
        const session: AuthSession = {
          internalJwt: response.token,
          expiresAt: response.expiresAt,
        };
        this.jwtStorage.store(session);
        this.session.set(session);
        return session;
      }),
      catchError((err) => throwError(() => err))
    );
  }

  logout(): Observable<void> {
    this.jwtStorage.clear();
    this.session.set(null);
    return this.msalFacade.logout();
  }
}
