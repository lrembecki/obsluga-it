import { Routes } from '@angular/router';
import { routeTemplate } from 'app/core/helpers/route.helper';
import {
  injectServices,
  provideServices,
} from './individual-requests.provider';

export const routes: Routes = [
  routeTemplate({
    providers: [provideServices()],
    serviceProvider: () => injectServices(),
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        loadComponent: () =>
          import('./individual-requests-list').then(
            (e) => e.IndividualRequestsList,
          ),
      },
      {
        path: ':contactRequestId',
        loadComponent: () =>
          import('./individual-requests-details').then(
            (e) => e.IndividualRequestsDetails,
          ),
      },
    ],
  }),
];
