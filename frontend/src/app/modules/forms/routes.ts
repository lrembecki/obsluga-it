import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { SettingsFormDefinitionFacade } from '../settings/form-definitions/form-definition.facade';
import { FormsDataTableService } from './forms-data-table.service';
import { FormsFacade } from './forms.facade';

export const routes: Routes = [
  {
    path: '',
    data: {
      label: 'Form Entries',
    },
    providers: [
      SettingsFormDefinitionFacade,
      provideApiFacade(FormsFacade),
      provideDataTableService(FormsDataTableService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled(
          [inject(FormsFacade), inject(SettingsFormDefinitionFacade)].map((e) =>
            e.initialize(),
          ),
        );
      },
    },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: ':form-definitionId',
        data: {
          label: 'Form Entries',
        },
        loadComponent: () =>
          import('app/shared/data-table/data-table.template').then(
            (m) => m.DataTableTemplate,
          ),
      },
    ],
  },
];
