import { ApplicationConfig, APP_INITIALIZER, provideZonelessChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MessageService } from 'primeng/api';
import { appRoutes } from './app.routes';
import { internalJwtInterceptor, AuthFacade } from '@obsluga-it/core/auth';
import { NavbarService } from '@obsluga-it/core/navigation';

function initializeAuth(authFacade: AuthFacade): () => void {
  return () => authFacade.initialize();
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideZonelessChangeDetection(),
    provideRouter(appRoutes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([internalJwtInterceptor])),
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
