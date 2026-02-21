import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { SettingsCompanyDataTableService } from './company-data-table.service';
import { SettingsCompanyFormService } from './company-form.service';
import { SettingsCompanyFacade } from './company.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(SettingsCompanyFacade),
      provideDataTableService(SettingsCompanyDataTableService),
      provideFormService(SettingsCompanyFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled([inject(SettingsCompanyFacade)].map((e) => e.initialize()));
      },
    },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
      { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
    ],
  },
];
