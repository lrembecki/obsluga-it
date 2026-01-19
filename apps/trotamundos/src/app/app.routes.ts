import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'o-nas',
    loadComponent: () => import('./features/pages/about-us/about-us').then((m) => m.AboutUs),
  },
  {
    path: 'jak-to-dziala',
    loadComponent: () =>
      import('./features/pages/how-it-works/how-it-works').then((m) => m.HowItWorks),
  },
];
