import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { TrotamundosHowItWorksDataTableService } from './how-it-works-data-table.service';
import { TrotamundosHowItWorksFormService } from './how-it-works-form.service';
import { TrotamundosHowItWorksFacade } from './how-it-works.facade';

export const routes: Routes = [{
  path: '',
  providers: [
    provideArrayApiFacade(TrotamundosHowItWorksFacade),
    provideDataTableService(TrotamundosHowItWorksDataTableService),
    provideFormService(TrotamundosHowItWorksFormService),
  ],
  resolve: { _init: () => { Promise.allSettled([inject(TrotamundosHowItWorksFacade)].map((e) => e.initialize())); } },
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
    { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
  ],
}];
