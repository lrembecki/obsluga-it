import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { ContactsFacade } from './contact.facade';
import { SettingsContactDataTableService } from './contact-data-table.service';
import { SettingsContactFormService } from './contact-form.service';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(ContactsFacade),
      provideDataTableService(SettingsContactDataTableService),
      provideFormService(SettingsContactFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled([inject(ContactsFacade)].map((e) => e.initialize()));
      },
    },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        loadComponent: () =>
          import('app/shared/data-table/data-table.template').then(
            (m) => m.DataTableTemplate,
          ),
      },
      {
        path: ':id',
        loadComponent: () =>
          import('app/shared/forms/form-template').then((m) => m.FormTemplate),
      },
    ],
  },
];
