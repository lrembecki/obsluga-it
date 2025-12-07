import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { SecurityPermissionGroupFacade } from '../permission-groups/permission-group.provider';
import { SecurityPermissionFacade } from '../permissions/permission.provider';
import { AdministrationAccountSubscriptionDataTableService } from './account-subscription-data-table.service';
import { AdministrationAccountSubscriptionFormService } from './account-subscription-form.service';
import { SecuritySubscriptionAccountFacade } from './account-subscription.provider';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideApiFacade(SecuritySubscriptionAccountFacade),
      SecurityPermissionGroupFacade,
      SecurityPermissionFacade,
      provideDataTableService(
        AdministrationAccountSubscriptionDataTableService,
      ),
      provideFormService(AdministrationAccountSubscriptionFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled(
          [
            inject(SecuritySubscriptionAccountFacade),
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
