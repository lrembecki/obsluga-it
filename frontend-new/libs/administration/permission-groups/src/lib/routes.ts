import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { AdministrationPermissionGroupDataTableService } from './permission-group-data-table.service';
import { AdministrationPermissionGroupFormService } from './permission-group-form.service';
import { SecurityPermissionGroupFacade } from './permission-group.provider';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(SecurityPermissionGroupFacade),
      provideDataTableService(AdministrationPermissionGroupDataTableService),
      provideFormService(AdministrationPermissionGroupFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled(
          [inject(SecurityPermissionGroupFacade)].map((e) => e.initialize()),
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
