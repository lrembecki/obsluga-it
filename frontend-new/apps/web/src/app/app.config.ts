import {
  ApplicationConfig,
  APP_INITIALIZER,
  provideZonelessChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MessageService } from 'primeng/api';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';
import { appRoutes } from './app.routes';
import { internalJwtInterceptor, AuthFacade } from '@obsluga-it/core/auth';
import { API_BASE_URL } from '@obsluga-it/core/api';
import { NavbarService } from '@obsluga-it/core/navigation';
import { provideMsal } from './msal.config';
import { environment } from '../environments/environment';

function initializeAuth(authFacade: AuthFacade): () => void {
  return () => authFacade.initialize();
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideZonelessChangeDetection(),
    provideRouter(appRoutes),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Aura,
      },
    }),
    provideHttpClient(withInterceptors([internalJwtInterceptor])),
    { provide: API_BASE_URL, useValue: environment.apiUrl },
    provideMsal(),
    MessageService,
    NavbarService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeAuth,
      deps: [AuthFacade],
      multi: true,
    },
  ],
};
