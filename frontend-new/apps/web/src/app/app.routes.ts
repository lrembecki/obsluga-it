import { Routes } from '@angular/router';

export const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'users',
    pathMatch: 'full',
  },
  {
    path: 'users',
    loadChildren: () =>
      import('@obsluga-it/users/feature').then((m) => m.usersRoutes),
  },
  {
    path: '**',
    redirectTo: 'users',
  },
];
