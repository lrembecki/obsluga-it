import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'sign-in',
    loadComponent: () => import('./sign-in').then((m) => m.SignInComponent),
  },
  {
    path: 'unauthorized',
    loadComponent: () => import('./unauthorized').then((m) => m.UnauthorizedComponent),
  },
  {
    path: 'forbidden',
    loadComponent: () => import('./forbidden').then((m) => m.ForbiddenComponent),
  },
];
