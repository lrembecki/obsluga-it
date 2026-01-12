import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { ContactsFacade } from '../contacts/contact.facade';
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
        Promise.allSettled(
          [inject(SettingsCompanyFacade), inject(ContactsFacade)].map((e) =>
            e.initialize(),
          ),
        );
      },
    },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        providers: [provideDataTableService(SettingsCompanyDataTableService)],
        loadComponent: () =>
          import('app/shared/data-table/data-table.template').then(
            (m) => m.DataTableTemplate,
          ),
      },
      {
        path: ':id',
        providers: [provideFormService(SettingsCompanyFormService)],
        loadComponent: () =>
          import('app/shared/forms/form-template').then((m) => m.FormTemplate),
      },
    ],
  },
];
