import { Routes } from '@angular/router';

import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';

import { inject } from '@angular/core';
import { provideFormService } from '@app/shared/forms/form.service';
import { TrotamundosLoyalityProgramFormService } from './loaylity-program-form.service';
import { TrotamundosLoyalityProgramDataTableService } from './loyality-program-data-table.service';
import { TrotamundosLoyalityProgramFacade } from './loyality-program.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(TrotamundosLoyalityProgramFacade),
      provideDataTableService(TrotamundosLoyalityProgramDataTableService),
      provideFormService(TrotamundosLoyalityProgramFormService),
    ],
    resolve: {
      init: async () => {
        await Promise.all([
          inject(TrotamundosLoyalityProgramFacade).populate(),
        ]);
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
