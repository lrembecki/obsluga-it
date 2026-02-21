import { ApplicationConfig, APP_INITIALIZER } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { appRoutes } from './app.routes';
import { internalJwtInterceptor, AuthFacade } from '@obsluga-it/core/auth';

function initializeAuth(authFacade: AuthFacade): () => void {
  return () => authFacade.initialize();
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(appRoutes),
    provideHttpClient(withInterceptors([internalJwtInterceptor])),
    {
      provide: APP_INITIALIZER,
      useFactory: initializeAuth,
      deps: [AuthFacade],
      multi: true,
    },
  ],
};
