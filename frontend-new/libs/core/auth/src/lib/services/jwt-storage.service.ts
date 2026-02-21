import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { AuthSession } from '../models/auth-session.model';

const JWT_STORAGE_KEY = 'internal_jwt';

@Injectable({ providedIn: 'root' })
export class JwtStorageService {
  private readonly platformId = inject(PLATFORM_ID);

  store(session: AuthSession): void {
    if (!isPlatformBrowser(this.platformId)) return;
    localStorage.setItem(JWT_STORAGE_KEY, JSON.stringify(session));
  }

  retrieve(): AuthSession | null {
    if (!isPlatformBrowser(this.platformId)) return null;
    const raw = localStorage.getItem(JWT_STORAGE_KEY);
    if (!raw) return null;
    try {
      return JSON.parse(raw) as AuthSession;
    } catch {
      return null;
    }
  }

  clear(): void {
    if (!isPlatformBrowser(this.platformId)) return;
    localStorage.removeItem(JWT_STORAGE_KEY);
  }
}
