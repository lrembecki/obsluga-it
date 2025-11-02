import { Routes } from '@angular/router';
import { SubscriptionTableService } from './subscription.table';
import { TableService } from 'app/shared/ui/table/table.service';
import { ButtonPanelService } from 'app/shared/ui/button-panel/button-panel';
import { SubscriptionButtonPanelService } from './subscription.button-panel';
import { inject } from '@angular/core';
import { SubscriptionFacade } from 'app/core/facades/subscription.facade';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('app/shared/ui/list/list').then((e) => e.List),
    resolve: {
      init: () => inject(SubscriptionFacade).populate(),
    },
    providers: [
      { provide: TableService, useExisting: SubscriptionTableService },
      {
        provide: ButtonPanelService,
        useExisting: SubscriptionButtonPanelService,
      },
    ],
  },
];
