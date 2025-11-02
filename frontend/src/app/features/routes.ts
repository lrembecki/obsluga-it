import { Routes } from '@angular/router';
import { authorizedGuard } from 'app/core/guards/authorized-guard';
import { withBreadcrumbs } from 'app/core/helpers/breadcrumbs.helper';
import { provideSubscription } from './security/subscriptions/subscription.provider';

export const routes: Routes = withBreadcrumbs([
  {
    path: 'files',
    loadChildren: () =>
      import('app/features/files/routes').then((e) => e.routes),
    data: {
      label: 'FILES',
      permissions: ['Files'],
    },
    canActivate: [authorizedGuard(['Files'])],
  },
  {
    path: 'images',
    loadChildren: () =>
      import('app/features/images/routes').then((e) => e.routes),
    data: {
      label: 'IMAGES',
      permissions: ['Images'],
    },
    canActivate: [authorizedGuard(['Images'])],
  },
  {
    path: 'locations',
    loadChildren: () =>
      import('app/features/locations/routes').then((e) => e.routes),
    data: {
      label: 'LOCATIONS',
      permissions: ['Locations'],
    },
    canActivate: [authorizedGuard(['Locations'])],
  },
  {
    path: 'trip-requests',
    loadChildren: () =>
      import('app/features/trip-requests/routes').then((e) => e.routes),
    data: {
      label: 'TRIP_REQUESTS',
      permissions: ['TripRequests'],
    },
    canActivate: [authorizedGuard(['TripRequests'])],
  },
  {
    path: 'security',
    loadChildren: () => import('./security/routes').then((e) => e.routes),
    providers: [provideSubscription()],
    data: {
      label: 'SECURITY.NAME',
    },
  },
]);
