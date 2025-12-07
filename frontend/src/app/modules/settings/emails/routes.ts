import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { SettingsEmailDataTableService } from './email-data-table.service';
import { SettingsEmailFormService } from './email-form.service';
import { SettingsEmailFacade } from './email.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(SettingsEmailFacade),
      provideDataTableService(SettingsEmailDataTableService),
      provideFormService(SettingsEmailFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled(
          [inject(SettingsEmailFacade)].map((e) => e.initialize()),
        );
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
