import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { TrotamundosAdvantageFacade } from '../advantages/advantage.facade';
import { TrotamundosHighlightFacade } from '../highlights/highlight.facade';
import { TrotamundosTripDataTableService } from './trip-data-table.service';
import { TrotamundosTripFormService } from './trip-form.service';
import { TrotamundosTripFacade } from './trip.provider';

export const routes: Routes = [
  {
    path: '',
    providers: [
      TrotamundosAdvantageFacade,
      TrotamundosHighlightFacade,
      provideApiFacade(TrotamundosTripFacade),
      provideDataTableService(TrotamundosTripDataTableService),
      provideFormService(TrotamundosTripFormService),
    ],
    resolve: {
      _init: () =>
        Promise.allSettled(
          [
            inject(TrotamundosAdvantageFacade),
            inject(TrotamundosTripFacade),
            inject(TrotamundosHighlightFacade),
          ].map((e) => e.initialize()),
        ),
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
