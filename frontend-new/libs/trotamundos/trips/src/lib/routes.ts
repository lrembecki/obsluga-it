import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { TrotamundosTripDataTableService } from './trip-data-table.service';
import { TrotamundosTripFormService } from './trip-form.service';
import { TrotamundosTripFacade } from './trip.facade';

export const routes: Routes = [{
  path: '',
  providers: [
    provideArrayApiFacade(TrotamundosTripFacade),
    provideDataTableService(TrotamundosTripDataTableService),
    provideFormService(TrotamundosTripFormService),
  ],
  resolve: { _init: () => { Promise.allSettled([inject(TrotamundosTripFacade)].map((e) => e.initialize())); } },
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
    { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
  ],
}];
