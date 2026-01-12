import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { SettingsCompanyFacade } from '../companies/company.facade';
import { SettingsWebsiteDataTableService } from './website-data-table.service';
import { SettingsWebsiteFormService } from './website-form.service';
import { SettingsWebsiteFacade } from './website.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [provideArrayApiFacade(SettingsWebsiteFacade)],
    resolve: {
      _init: () =>
        Promise.allSettled(
          [inject(SettingsWebsiteFacade), inject(SettingsCompanyFacade)].map(
            (ArrayFacade) => ArrayFacade.initialize(),
          ),
        ),
    },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        providers: [provideDataTableService(SettingsWebsiteDataTableService)],
        loadComponent: () =>
          import('app/shared/data-table/data-table.template').then(
            (m) => m.DataTableTemplate,
          ),
      },
      {
        path: ':id',
        providers: [provideFormService(SettingsWebsiteFormService)],
        loadComponent: () =>
          import('app/shared/forms/form-template').then((m) => m.FormTemplate),
      },
    ],
  },
];
