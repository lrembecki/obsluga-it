import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { FormsDataTableService } from './forms-data-table.service';
import { FormsFormService } from './forms-form.service';
import { FormsFacade } from './forms.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(FormsFacade),
      provideDataTableService(FormsDataTableService),
      provideFormService(FormsFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled([inject(FormsFacade)].map((e) => e.initialize()));
      },
    },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        loadComponent: () =>
          import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate),
      },
      {
        path: ':id',
        loadComponent: () =>
          import('@obsluga-it/shared/forms').then((m) => m.FormTemplate),
      },
    ],
  },
];
