import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { SettingsWebsiteDataTableService } from './website-data-table.service';
import { SettingsWebsiteFormService } from './website-form.service';
import { SettingsWebsiteFacade } from './website.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(SettingsWebsiteFacade),
      provideDataTableService(SettingsWebsiteDataTableService),
      provideFormService(SettingsWebsiteFormService),
    ],
    resolve: { _init: () => { Promise.allSettled([inject(SettingsWebsiteFacade)].map((e) => e.initialize())); } },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
      { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
    ],
  },
];
