import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { TagsFacade } from './core/facades/tags.facade';
import { authorizedGuard } from './core/guards/authorized-guard';

export const routes: Routes = [
  {
    path: 'account',
    loadChildren: () =>
      import('./features/account/routes').then((e) => e.routes),
  },
  {
    path: 'forms',
    loadChildren: () => import('./forms/routes').then((e) => e.routes),
  },
  {
    path: 'modules',
    loadChildren: () => import('./modules/routes').then((e) => e.routes),
  },
  {
    path: '',
    loadChildren: () => import('app/features/routes').then((e) => e.routes),
    resolve: {
      init: async () =>
        Object.values({
          tags: inject(TagsFacade),
        }).map((e) => e.populate()),
    },
    canActivate: [authorizedGuard([])],
    canActivateChild: [authorizedGuard([])],
  },
];
