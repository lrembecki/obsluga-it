import { Routes } from '@angular/router';

const facades = {
  administration: await import('app/modules/administration/routes').then(
    (e) => e.facades,
  ),
  forms: await import('app/modules/forms/routes').then((e) => e.facades),
  settings: await import('app/modules/settings/routes').then((e) => e.facades),
  trotamundos: await import('app/modules/trotamundos/routes').then(
    (e) => e.facades,
  ),
};

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
    providers: Object.values(facades)
      .flatMap((f) => Object.values(f))
      .flat(),
    loadChildren: () => import('./modules/routes').then((e) => e.routes),
  },
];
