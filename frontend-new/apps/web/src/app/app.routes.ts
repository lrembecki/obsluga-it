import { Routes } from '@angular/router';

export const modulesRoutes: Routes = [
  {
    path: 'administration',
    data: { title: 'Administration' },
    loadChildren: () =>
      import('../../../../libs/administration/routes').then((m) => m.administrationRoutes),
  },
  {
    path: 'trotamundos',
    data: { title: 'Trotamundos' },
    loadChildren: () =>
      import('../../../../libs/trotamundos/routes').then((m) => m.trotamundosRoutes),
  },
  {
    path: 'settings',
    data: { title: 'Settings' },
    loadChildren: () =>
      import('../../../../libs/settings/routes').then((m) => m.settingsRoutes),
  },
  {
    path: 'forms',
    data: { title: 'Forms' },
    loadChildren: () =>
      import('@obsluga-it/forms').then((m) => m.routes),
  },
].sort((a, b) => (a.data?.['title'] ?? '').localeCompare(b.data?.['title'] ?? ''));

export const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'modules',
    pathMatch: 'full',
  },
  {
    path: 'account',
    loadChildren: () =>
      import('@obsluga-it/account').then((m) => m.routes),
  },
  {
    path: 'users',
    loadChildren: () =>
      import('@obsluga-it/users/feature').then((m) => m.usersRoutes),
  },
  {
    path: 'modules',
    children: modulesRoutes,
  },
  {
    path: '**',
    redirectTo: 'modules',
  },
];
