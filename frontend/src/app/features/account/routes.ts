import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'sign-in',
    loadComponent: () => import('./sign-in').then((m) => m.SignIn),
  },
  {
    path: 'unauthorized',
    loadComponent: () => import('./unauthorized').then((m) => m.Unauthorized),
  },
  {
    path: 'forbidden',
    loadComponent: () => import('./forbidden').then((m) => m.Forbidden),
  },
  {
    path: '',
    redirectTo: 'sign-in',
    pathMatch: 'full',
  },
];
