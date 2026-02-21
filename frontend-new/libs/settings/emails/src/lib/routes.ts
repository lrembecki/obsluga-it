import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { SettingsEmailDataTableService } from './email-data-table.service';
import { SettingsEmailFormService } from './email-form.service';
import { SettingsEmailFacade } from './email.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(SettingsEmailFacade),
      provideDataTableService(SettingsEmailDataTableService),
      provideFormService(SettingsEmailFormService),
    ],
    resolve: { _init: () => { Promise.allSettled([inject(SettingsEmailFacade)].map((e) => e.initialize())); } },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
      { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
    ],
  },
];
