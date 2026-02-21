import { Injectable, signal, computed } from '@angular/core';
import { AuthSession } from '@obsluga-it/core/auth';

@Injectable({ providedIn: 'root' })
export class GlobalSessionStore {
  private readonly _session = signal<AuthSession | null>(null);

  readonly session = this._session.asReadonly();
  readonly isAuthenticated = computed(() => this._session() !== null);

  setSession(session: AuthSession | null): void {
    this._session.set(session);
  }
}
