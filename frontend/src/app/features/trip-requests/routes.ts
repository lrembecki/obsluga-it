import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { TripRequestFacade } from 'app/core/facades/trip-request.facade';
import { withBreadcrumbs } from 'app/core/helpers/breadcrumbs.helper';
import { TableService } from '../../shared/ui/table/table.service';
import { TripRequestsTableService } from './trip-requests.table';

export const routes: Routes = withBreadcrumbs([
  {
    path: '',
    loadComponent: () => import('app/shared/ui/list/list').then((e) => e.List),
    resolve: {
      init: () => {
        const facades = { files: inject(TripRequestFacade) };
        Promise.allSettled(Object.values(facades).map((e) => e.initialize()));
      },
    },
    providers: [
      TripRequestsTableService,
      { provide: TableService, useExisting: TripRequestsTableService },
    ],
  },
  {
    path: ':tripRequestId',
    loadComponent: () =>
      import('./trip-requests.form').then((e) => e.TripRequestForm),
    resolve: {
      model: async (state: ActivatedRouteSnapshot) => {
        const files = inject(TripRequestFacade);

        await files.initialize();

        const tripRequestId = state['params']!['tripRequestId'] as string;
        return files.data().find((e) => e.tripRequestId === tripRequestId)!;
      },
    },
  },
]);
