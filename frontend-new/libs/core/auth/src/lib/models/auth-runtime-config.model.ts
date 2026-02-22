import { InjectionToken } from '@angular/core';

export interface AuthRuntimeConfig {
  authenticateEndpoint: string;
  msalScopes: string[];
}

export const AUTH_RUNTIME_CONFIG = new InjectionToken<AuthRuntimeConfig>(
  'AUTH_RUNTIME_CONFIG'
);
