import { Routes } from '@angular/router';

import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { TrotamundosFileDataTableService } from './file-data-table.service';
import { TrotamundosFileFormService } from './file-form.service';
import { TrotamundosFileFacade } from './file.provider';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(TrotamundosFileFacade),
      provideDataTableService(TrotamundosFileDataTableService),
      provideFormService(TrotamundosFileFormService),
    ],
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        loadComponent: () =>
          import('app/shared/data-table/data-table.template').then(
            (e) => e.DataTableTemplate,
          ),
      },
      {
        path: ':id',
        loadComponent: () =>
          import('app/shared/forms/form-template').then((e) => e.FormTemplate),
      },
    ],
  },
];
