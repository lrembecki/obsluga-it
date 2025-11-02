import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'subscriptions',
    pathMatch: 'full',
  },
  {
    path: 'subscriptions',
    loadChildren: () => import('./subscriptions/routes').then((e) => e.routes),
    data: {
      label: 'SUBSCRIPTIONS.NAME',
    },
  },
];
