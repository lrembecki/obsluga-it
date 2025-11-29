import { Routes } from '@angular/router';

import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { TrotamundosHighlightDataTableService } from './highlight-data-table.service';
import { TrotamundosHighlightFormService } from './highlight-form.service';
import { TrotamundosHighlightFacade } from './highlight.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(TrotamundosHighlightFacade),
      provideFormService(TrotamundosHighlightFormService),
      provideDataTableService(TrotamundosHighlightDataTableService),
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
