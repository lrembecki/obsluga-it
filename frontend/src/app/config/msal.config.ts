import {
  EnvironmentProviders,
  importProvidersFrom,
  Provider,
} from '@angular/core';
import {
  MSAL_GUARD_CONFIG,
  MSAL_INSTANCE,
  MSAL_INTERCEPTOR_CONFIG,
  MsalGuard,
  MsalGuardConfiguration,
  MsalInterceptorConfiguration,
  MsalModule,
  MsalService,
} from '@azure/msal-angular';
import {
  BrowserCacheLocation,
  InteractionType,
  LogLevel,
  PublicClientApplication,
} from '@azure/msal-browser';
import { environment } from 'environments/environment';

const isIE =
  window.navigator.userAgent.indexOf('MSIE ') > -1 ||
  window.navigator.userAgent.indexOf('Trident/') > -1;

function MSALInstanceFactory(): PublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: environment.msal.clientId,
      authority: environment.msal.authority,
      redirectUri: `${environment.msal.redirectUri}`,
      postLogoutRedirectUri: environment.msal.postLogoutRedirectUri,
      knownAuthorities: environment.msal.knownAuthorities,
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: isIE,
    },
    system: {
      loggerOptions: {
        loggerCallback: (level: LogLevel, message: string) => {
          // Filter out performance trace errors and 404s
          // if (message.includes('perftrace') || message.includes('404')) {
          //   return;
          // }
          if (!environment.production) {
            console.log({ level, message });
          }
        },
        piiLoggingEnabled: false,
        logLevel: environment.production ? LogLevel.Error : LogLevel.Warning,
      },
      windowHashTimeout: 60000,
      iframeHashTimeout: 6000,
      loadFrameTimeout: 0,
    },
  });
}

function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();

  // Map API endpoints to B2C scopes
  protectedResourceMap.set(
    environment.msal.protectedResources.productionApi,
    environment.msal.scopes.default,
  );

  // Add local development API endpoint
  protectedResourceMap.set(
    environment.msal.protectedResources.localApi,
    environment.msal.scopes.default,
  );

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap,
  };
}

function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: [
        ...environment.msal.scopes.consent,
        ...environment.msal.scopes.default,
      ],
    },
    loginFailedRoute: '/account/unauthorized',
  };
}

export function provideMsal(): (Provider | EnvironmentProviders)[] {
  return [
    importProvidersFrom(MsalModule),
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory,
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory,
    },
    MsalService,
    MsalGuard,
  ];
}
