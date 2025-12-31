import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { SettingsFormDefinitionFacade } from '../settings/form-definitions/form-definition.facade';
import { FormsDataTableService } from './forms-data-table.service';
import { FormsFormService } from './forms-form.service';
import { FormsFacade } from './forms.facade';

export const facades = {
  forms: await import('app/modules/forms/forms.facade').then(
    (e) => e.FormsFacade,
  ),
};

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(FormsFacade),
      provideDataTableService(FormsDataTableService),
      provideFormService(FormsFormService),
    ],
    resolve: {
      _: async () => {
        const facades = {
          formDefinitions: inject(SettingsFormDefinitionFacade),
          forms: inject(FormsFacade),
        };
        await Promise.allSettled(
          Object.values(facades).map((e) => e.initialize()),
        );
      },
    },
    children: [
      {
        path: ':formDefinitionId',
        resolve: {
          _: (state: ActivatedRouteSnapshot) => {
            inject(FormsFormService).formDefinitionId.set(
              state.params['formDefinitionId'],
            );
            inject(FormsDataTableService).formDefinitionId.set(
              state.params['formDefinitionId'],
            );
          },
        },
        children: [
          {
            path: '',
            redirectTo: 'list',
            pathMatch: 'full',
          },
          {
            path: 'list',
            data: {
              label: 'Form Entries',
            },
            loadComponent: () =>
              import('app/shared/data-table/data-table.template').then(
                (m) => m.DataTableTemplate,
              ),
          },
          {
            path: ':id',
            loadComponent: () =>
              import('app/shared/forms/form-template').then(
                (m) => m.FormTemplate,
              ),
          },
        ],
      },
    ],
  },
];
