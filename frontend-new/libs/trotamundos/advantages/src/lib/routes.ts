import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { TrotamundosAdvantageDataTableService } from './advantage-data-table.service';
import { TrotamundosAdvantageFormService } from './advantage-form.service';
import { TrotamundosAdvantageFacade } from './advantage.facade';

export const routes: Routes = [{
  path: '',
  providers: [
    provideArrayApiFacade(TrotamundosAdvantageFacade),
    provideDataTableService(TrotamundosAdvantageDataTableService),
    provideFormService(TrotamundosAdvantageFormService),
  ],
  resolve: { _init: () => { Promise.allSettled([inject(TrotamundosAdvantageFacade)].map((e) => e.initialize())); } },
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
    { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
  ],
}];
