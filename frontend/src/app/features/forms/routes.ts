import { Routes } from '@angular/router';
import { injectForms, provideForms } from './forms.provider';

export const routes: Routes = [
  {
    path: '',
    providers: [provideForms()],
    resolve: {
      services: () => injectForms(),
    },
    loadComponent: () =>
      import('app/shared/templates/feature-template').then(
        (e) => e.FeatureTemplate,
      ),
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      {
        path: 'list',
        loadComponent: () => import('./forms-list').then((e) => e.FormsList),
      },
      {
        path: ':formId',
        loadChildren: () =>
          import('./form-details/routes').then((e) => e.routes),
      },
    ],
  },
];
