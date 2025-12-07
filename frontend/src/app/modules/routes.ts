import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'administration',
    data: {
      title: 'Administration',
    },
    loadChildren: () => import('./administration/routes').then((m) => m.routes),
  },
  {
    path: 'trotamundos',
    data: {
      title: 'Trotamundos',
    },
    loadChildren: () => import('./trotamundos/routes').then((m) => m.routes),
  },
  {
    path: 'settings',
    data: {
      title: 'Settings',
    },
    loadChildren: () => import('./settings/routes').then((m) => m.routes),
  },
];
