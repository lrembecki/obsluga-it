export const environment = {
  production: false,
  apiUrl: 'https://localhost:7043/api',
  assetsUrl: '/share',
  features: {
    enableThemeSwitcher: true,
    enableAnalytics: false,
  },
  msal: {
    clientId: '6e69a6f9-7efa-4eb3-8a8c-1812f44f628a',
    authority:
      'https://platformproduction.b2clogin.com/platformproduction.onmicrosoft.com/B2C_1_susi',
    redirectUri: window.location.origin,
    postLogoutRedirectUri: window.location.origin,
    knownAuthorities: ['platformproduction.b2clogin.com'],
    scopes: {
      default: [
        'https://platformproduction.onmicrosoft.com/platform/access_as_user',
        'https://platformproduction.onmicrosoft.com/platform/consent',
      ],
      consent: ['https://platformproduction.onmicrosoft.com/platform/consent'],
    },
    protectedResources: {
      productionApi:
        'https://api-obsluga-it-prd-01.azurewebsites.net/api/authenticate',
      localApi: 'https://localhost:7043/api/authenticate',
    },
  },
};
