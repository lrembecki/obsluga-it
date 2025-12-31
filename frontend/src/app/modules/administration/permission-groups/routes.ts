import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { SecurityPermissionFacade } from '../permissions/permission.provider';
import { AdministrationPermissionGroupDataTableService } from './permission-group-data-table.service';
import { AdministrationPermissionGroupFormService } from './permission-group-form.service';
import { SecurityPermissionGroupFacade } from './permission-group.provider';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(SecurityPermissionGroupFacade),
      provideDataTableService(AdministrationPermissionGroupDataTableService),
      provideFormService(AdministrationPermissionGroupFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled(
          [
            inject(SecurityPermissionGroupFacade),
            inject(SecurityPermissionFacade),
          ].map((e) => e.initialize()),
        );
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
