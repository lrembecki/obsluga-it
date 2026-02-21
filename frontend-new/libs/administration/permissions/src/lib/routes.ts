import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { AdministrationPermissionDataTableService } from './permission-data-table.service';
import { AdministrationPermissionFormService } from './permission-form.service';
import { SecurityPermissionFacade } from './permission.provider';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(SecurityPermissionFacade),
      provideDataTableService(AdministrationPermissionDataTableService),
      provideFormService(AdministrationPermissionFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled(
          [inject(SecurityPermissionFacade)].map((e) => e.initialize()),
        );
      },
    },
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        loadComponent: () =>
          import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate),
      },
      {
        path: ':id',
        loadComponent: () =>
          import('@obsluga-it/shared/forms').then((m) => m.FormTemplate),
      },
    ],
  },
];
