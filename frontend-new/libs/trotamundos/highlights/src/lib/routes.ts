import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { TrotamundosHighlightDataTableService } from './highlight-data-table.service';
import { TrotamundosHighlightFormService } from './highlight-form.service';
import { TrotamundosHighlightFacade } from './highlight.facade';

export const routes: Routes = [{
  path: '',
  providers: [
    provideArrayApiFacade(TrotamundosHighlightFacade),
    provideDataTableService(TrotamundosHighlightDataTableService),
    provideFormService(TrotamundosHighlightFormService),
  ],
  resolve: { _init: () => { Promise.allSettled([inject(TrotamundosHighlightFacade)].map((e) => e.initialize())); } },
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
    { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
  ],
}];
