import { Routes } from '@angular/router';
import { routeTemplate } from 'app/core/helpers/route.helper';
import { injectServices, provideServices } from './email-settings.provider';

export const routes: Routes = [
  routeTemplate({
    providers: [provideServices()],
    serviceProvider: () => injectServices(),
    children: [
      {
        path: '',
        loadComponent: () => import('./email-form').then((e) => e.EmailForm),
      },
    ],
  }),
];
