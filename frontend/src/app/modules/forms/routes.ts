import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { FormsDataTableService } from './forms-data-table.service';
import { FormsFacade } from './forms.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(FormsFacade),
      provideDataTableService(FormsDataTableService),
    ],
    children: [
      {
        path: ':formDefinitionId',
        redirectTo: ':formDefinitionId/list',
        pathMatch: 'full',
      },
      {
        path: ':formDefinitionId/list',
        data: {
          label: 'Form Entries',
        },
        resolve: {
          _: (state: ActivatedRouteSnapshot) => {
            inject(FormsDataTableService).formDefinitionId.set(
              state.params['formDefinitionId'],
            );
          },
        },
        loadComponent: () =>
          import('app/shared/data-table/data-table.template').then(
            (m) => m.DataTableTemplate,
          ),
      },
    ],
  },
];
