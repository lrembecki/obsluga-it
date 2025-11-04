import {
  provideHttpClient,
  withInterceptors,
  withInterceptorsFromDi,
} from '@angular/common/http';
import {
  ApplicationConfig,
  isDevMode,
  LOCALE_ID,
  provideAppInitializer,
  provideBrowserGlobalErrorListeners,
  provideZonelessChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';
import Aura from '@primeuix/themes/aura';

import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideServiceWorker } from '@angular/service-worker';
import { MessageService } from 'primeng/api';
import { providePrimeNG } from 'primeng/config';
import { routes } from './app.routes';
import { provideMsal } from './config/msal.config';
import { TagsFacade } from './core/facades/tags.facade';
import {
  detectLocale,
  loadAndRegisterLocale,
} from './core/helpers/locale.helper';
import { apiInterceptor } from './core/interceptors/api-interceptor';
import { imageCacheInterceptor } from './core/interceptors/image-cache.interceptor';
import { TranslatePipe } from './core/pipes/translate.pipe';
import { provideTranslation } from './core/providers/translation.providers';
import { ApiService } from './core/services/api.service';
import { AppUpdateService } from './core/services/app-update.service';
import { NotificationService } from './core/services/notification.service';

let RUNTIME_LOCALE = 'en-US';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideRouter(routes),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Aura,
        options: {
          prefix: 'p',
          darkModeSelector: '.app-dark',
          cssLayer: false,
        },
      },
    }),
    provideMsal(),
    provideTranslation(),
    provideHttpClient(
      withInterceptors([apiInterceptor, imageCacheInterceptor]),
      withInterceptorsFromDi(),
    ),
    MessageService,
    NotificationService,
    AppUpdateService,
    TranslatePipe,
    ApiService,
    TagsFacade,
    provideServiceWorker('ngsw-worker.js', {
      enabled: !isDevMode(),
      registrationStrategy: 'registerWhenStable:30000',
    }),
    provideAppInitializer(async () => {
      const detected = detectLocale();
      RUNTIME_LOCALE = await loadAndRegisterLocale(detected);
    }),
    {
      provide: LOCALE_ID,
      useFactory: () => RUNTIME_LOCALE,
    },
  ],
};
