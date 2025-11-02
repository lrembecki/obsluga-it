export const environment = {
  production: false,
  apiUrl: 'https://api-obsluga-it-prd-01.azurewebsites.net/api',
  assetsUrl: '/share',
  features: {
    enableThemeSwitcher: true,
    enableAnalytics: false,
  },
  msal: {
    clientId: 'd56545db-3026-46f3-9912-470603e99135',
    authority:
      'https://platformproduction.b2clogin.com/platformproduction.onmicrosoft.com/B2C_1_susi',
    redirectUri: 'https://web-obsluga-it-prd-01.azurewebsites.net/',
    postLogoutRedirectUri: 'https://web-obsluga-it-prd-01.azurewebsites.net/',
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
      localApi: 'https://api-obsluga-it-prd-01.azurewebsites.net/api/authenticate',
    },
  },
};
