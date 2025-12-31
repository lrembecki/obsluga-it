import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { ContactsFacade } from '../contacts/contact.facade';
import { SettingsEmailTemplateDataTableService } from './email-template-data-table.service';
import { SettingsEmailTemplateFormService } from './email-template-form.service';
import { SettingsEmailTemplateFacade } from './email-template.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(SettingsEmailTemplateFacade),
      provideDataTableService(SettingsEmailTemplateDataTableService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled(
          [inject(SettingsEmailTemplateFacade), inject(ContactsFacade)].map(
            (e) => e.initialize(),
          ),
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
        providers: [provideFormService(SettingsEmailTemplateFormService)],
        path: ':id',
        loadComponent: () =>
          import('app/shared/forms/form-template').then((m) => m.FormTemplate),
      },
    ],
  },
];
