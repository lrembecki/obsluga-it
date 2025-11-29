import { Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { TrotamundosTripDataTableService } from './trip-data-table.service';
import { TrotamundosTripFormService } from './trip-form.service';
import { TrotamundosTripFacade } from './trip.provider';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(TrotamundosTripFacade),
      provideDataTableService(TrotamundosTripDataTableService),
      provideFormService(TrotamundosTripFormService),
    ],
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
