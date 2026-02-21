import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { AdministrationSubscriptionAccountDataTableService } from './account-subscription-data-table.service';
import { AdministrationSubscriptionAccountFormService } from './account-subscription-form.service';
import { SecuritySubscriptionAccountFacade } from './account-subscription.provider';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(SecuritySubscriptionAccountFacade),
      provideDataTableService(AdministrationSubscriptionAccountDataTableService),
      provideFormService(AdministrationSubscriptionAccountFormService),
    ],
    resolve: {
      _init: () => {
        Promise.allSettled(
          [inject(SecuritySubscriptionAccountFacade)].map((e) => e.initialize()),
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
