import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { TagsFacade } from '@app/core/facades/tags.facade';
import { authorizedGuard } from 'app/core/guards/authorized-guard';
import { withBreadcrumbs } from 'app/core/helpers/breadcrumbs.helper';

export const routes: Routes = withBreadcrumbs([
  {
    path: '',
    resolve: {
      init: async () =>
        Object.values({
          tags: inject(TagsFacade),
        }).map((e) => e.populate()),
    },
    children: [
      {
        path: 'files',
        loadChildren: () =>
          import('app/features/files/routes').then((e) => e.routes),
        data: {
          label: 'FILES',
          permissions: ['Files'],
        },
        canActivate: [authorizedGuard([])],
      },
      {
        path: 'images',
        loadChildren: () =>
          import('app/features/images/routes').then((e) => e.routes),
        data: {
          label: 'IMAGES',
          permissions: [],
        },
        canActivate: [authorizedGuard([])],
      },
      {
        path: 'locations',
        loadChildren: () =>
          import('app/features/locations/routes').then((e) => e.routes),
        data: {
          label: 'LOCATIONS',
          permissions: [],
        },
        canActivate: [authorizedGuard([])],
      },
      {
        path: 'trip-requests',
        loadChildren: () =>
          import('app/features/trip-requests/routes').then((e) => e.routes),
        data: {
          label: 'TRIP_REQUESTS',
          permissions: [],
        },
        canActivate: [authorizedGuard([])],
      },
      {
        path: 'security',
        loadChildren: () => import('./security/routes').then((e) => e.routes),
        providers: [],
        data: {
          label: 'SECURITY.NAME',
        },
      },
    ]
  }

]);
