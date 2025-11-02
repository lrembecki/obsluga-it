import { Routes } from '@angular/router';
import { routeTemplate } from 'app/core/helpers/route.helper';
import { injectServices, provideServices } from './contact-request.provider';

export const routes: Routes = [
  routeTemplate({
    providers: [provideServices()],
    serviceProvider: () => injectServices(),
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        loadComponent: () =>
          import('./contact-request').then((e) => e.ContactRequest),
      },
      {
        path: ':contactRequestId',
        loadComponent: () =>
          import('./contact-request-details').then(
            (e) => e.ContactRequestDetails,
          ),
      },
    ],
  }),
];
