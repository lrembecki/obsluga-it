import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { SettingsContactDataTableService } from './contact-data-table.service';
import { SettingsContactFormService } from './contact-form.service';
import { ContactsFacade } from './contact.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(ContactsFacade),
      provideDataTableService(SettingsContactDataTableService),
      provideFormService(SettingsContactFormService),
    ],
    resolve: {
      _init: () => { Promise.allSettled([inject(ContactsFacade)].map((e) => e.initialize())); },
    },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
      { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
    ],
  },
];
