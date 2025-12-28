import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { SettingsEmailTemplateFacade } from '../email-templates/email-template.facade';
import { SettingsEmailFacade } from '../emails/email.facade';
import { SettingsFormDefinitionDataTableService } from './form-definition-data-table.service';
import { SettingsFormDefinitionFormService } from './form-definition-form.service';
import { SettingsFormDefinitionFacade } from './form-definition.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      SettingsEmailFacade,
      SettingsEmailTemplateFacade,
      provideApiFacade(SettingsFormDefinitionFacade),
      provideDataTableService(SettingsFormDefinitionDataTableService),
      provideFormService(SettingsFormDefinitionFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled(
          [
            inject(SettingsFormDefinitionFacade),
            inject(SettingsEmailFacade),
            inject(SettingsEmailTemplateFacade),
          ].map((e) => e.initialize()),
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
