import { Injectable, signal } from '@angular/core';

export interface AppConfig {
  readonly apiBaseUrl: string;
  readonly msalClientId: string;
  readonly msalTenantId: string;
  readonly msalScopes: string[];
}

@Injectable({ providedIn: 'root' })
export class GlobalAppConfigStore {
  private readonly _config = signal<AppConfig | null>(null);

  readonly config = this._config.asReadonly();

  setConfig(config: AppConfig): void {
    this._config.set(config);
  }
}
