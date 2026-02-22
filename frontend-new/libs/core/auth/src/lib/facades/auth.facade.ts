import { inject, Injectable, Injector, signal } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map, catchError, throwError, switchMap } from 'rxjs';
import { MsalAuthFacade } from './msal-auth.facade';
import { JwtStorageService } from '../services/jwt-storage.service';
import { AuthSession } from '../models/auth-session.model';
import {
  AUTH_RUNTIME_CONFIG,
  AuthRuntimeConfig,
} from '../models/auth-runtime-config.model';

type AuthenticatePayload = {
  token?: string;
  expiresAt?: number | string;
  expires?: string;
};

type AuthenticateResponse =
  | AuthenticatePayload
  | {
      success?: boolean;
      data?: AuthenticatePayload;
    };

const DEFAULT_AUTH_RUNTIME_CONFIG: AuthRuntimeConfig = {
  authenticateEndpoint: 'https://localhost:7043/api/authenticate',
  msalScopes: [
    'https://platformproduction.onmicrosoft.com/platform/access_as_user',
    'https://platformproduction.onmicrosoft.com/platform/consent',
  ],
};

@Injectable({ providedIn: 'root' })
export class AuthFacade {
  private readonly http = inject(HttpClient);
  private readonly injector = inject(Injector);
  private readonly jwtStorage = inject(JwtStorageService);
  private readonly authConfig =
    inject(AUTH_RUNTIME_CONFIG, { optional: true }) ??
    DEFAULT_AUTH_RUNTIME_CONFIG;

  readonly session = signal<AuthSession | null>(null);

  private get msalFacade(): MsalAuthFacade {
    return this.injector.get(MsalAuthFacade);
  }

  private parseExpiresAt(value: number | string | undefined): number {
    if (typeof value === 'number') {
      return value;
    }

    if (typeof value === 'string') {
      const parsed = Date.parse(value);
      if (!Number.isNaN(parsed)) {
        return parsed;
      }
    }

    return Date.now() + 60 * 60 * 1000;
  }

  private toSession(response: AuthenticateResponse): AuthSession {
    let data: AuthenticatePayload;
    if ('data' in response) {
      data = response.data ?? {};
    } else {
      data = response as AuthenticatePayload;
    }

    const token = data.token;
    if (!token) {
      throw new Error('Authenticate response does not contain token');
    }

    return {
      internalJwt: token,
      expiresAt: this.parseExpiresAt(data.expiresAt ?? data.expires),
    };
  }

  initialize(): void {
    const stored = this.jwtStorage.retrieve();
    if (stored) {
      this.session.set(stored);
    }
  }

  authenticate(): Observable<AuthSession> {
    return this.msalFacade.acquireToken(this.authConfig.msalScopes).pipe(
      switchMap((msalToken) => {
        const headers = new HttpHeaders({
          Authorization: `Bearer ${msalToken}`,
        });
        return this.http.get<AuthenticateResponse>(
          this.authConfig.authenticateEndpoint,
          { headers }
        );
      }),
      map((response) => {
        const session = this.toSession(response);
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
