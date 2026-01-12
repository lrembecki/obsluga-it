import { Routes } from '@angular/router';

import { provideDataTableService } from '@app/shared/data-table/data-table.service';

import { provideArrayApiFacade } from '@app/core/interfaces/facade.interface';
import { provideFormService } from '@app/shared/forms/form.service';
import { TrotamundosAdvantageDataTableService } from './advantage-data-table.service';
import { TrotamundosAdvantageFormService } from './advantage-form.service';
import { TrotamundosAdvantageFacade } from './advantage.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(TrotamundosAdvantageFacade),
      provideDataTableService(TrotamundosAdvantageDataTableService),
      provideFormService(TrotamundosAdvantageFormService),
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
