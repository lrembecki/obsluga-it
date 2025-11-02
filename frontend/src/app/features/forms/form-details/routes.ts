import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./forms-details').then((e) => e.FormsDetails),
    children: [
      { path: '', redirectTo: 'requests', pathMatch: 'full' },
      {
        path: 'requests',
        loadChildren: () => import('./requests/routes').then((e) => e.routes),
      },
      {
        path: 'controls',
        loadChildren: () => import('./controls/routes').then((e) => e.routes),
      },
    ],
  },
];
