import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { TrotamundosAboutUsDataTableService } from './about-us-data-table.service';
import { TrotamundosAboutUsFormService } from './about-us-form.service';
import { TrotamundosAboutUsFacade } from './about-us.facade';

export const routes: Routes = [{
  path: '',
  providers: [
    provideArrayApiFacade(TrotamundosAboutUsFacade),
    provideDataTableService(TrotamundosAboutUsDataTableService),
    provideFormService(TrotamundosAboutUsFormService),
  ],
  resolve: { _init: () => { Promise.allSettled([inject(TrotamundosAboutUsFacade)].map((e) => e.initialize())); } },
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
    { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
  ],
}];
