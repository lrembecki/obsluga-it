import { Routes } from '@angular/router';
import { routeTemplate } from 'app/core/helpers/route.helper';
import { injectLocations, provideLocations } from './locations.provider';

export const routes: Routes = [
  routeTemplate({
    providers: [provideLocations()],
    serviceProvider: injectLocations,
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        loadComponent: () =>
          import('./locations-list').then((e) => e.LocationsList),
      },
      {
        path: 'create',
        loadComponent: () =>
          import('./locations-form').then((e) => e.LocationsForm),
      },
      {
        path: ':locationId',
        loadComponent: () =>
          import('./locations-form').then((e) => e.LocationsForm),
      },
    ],
  }),
];
