import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { SettingsFormDefinitionDataTableService } from './form-definition-data-table.service';
import { SettingsFormDefinitionFormService } from './form-definition-form.service';
import { SettingsFormDefinitionFacade } from './form-definition.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(SettingsFormDefinitionFacade),
      provideDataTableService(SettingsFormDefinitionDataTableService),
      provideFormService(SettingsFormDefinitionFormService),
    ],
    resolve: { _init: () => { Promise.allSettled([inject(SettingsFormDefinitionFacade)].map((e) => e.initialize())); } },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
      { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
    ],
  },
];
