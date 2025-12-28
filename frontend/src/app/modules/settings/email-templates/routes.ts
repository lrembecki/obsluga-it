import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { SettingsEmailTemplateDataTableService } from './email-template-data-table.service';
import { SettingsEmailTemplateFormService } from './email-template-form.service';
import { SettingsEmailTemplateFacade } from './email-template.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(SettingsEmailTemplateFacade),
      provideDataTableService(SettingsEmailTemplateDataTableService),
      provideFormService(SettingsEmailTemplateFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled(
          [inject(SettingsEmailTemplateFacade)].map((e) => e.initialize()),
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
