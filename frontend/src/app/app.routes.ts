import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'modules',
    pathMatch: 'full',
  },
  {
    path: 'account',
    loadChildren: () =>
      import('./modules/account/routes').then((e) => e.routes),
  },
  {
    path: 'modules',
    loadChildren: () => import('./modules/routes').then((e) => e.routes),
  },
];
