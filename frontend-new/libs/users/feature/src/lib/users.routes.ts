import { Route } from '@angular/router';
import { authGuard } from '@obsluga-it/core/auth';

export const usersRoutes: Route[] = [
  {
    path: '',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./users.page').then((m) => m.UsersPage),
  },
];
