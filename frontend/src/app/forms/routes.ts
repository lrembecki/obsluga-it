import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'contact-requests',
    data: {
      label: 'Contact',
      permissions: ['Files'],
    },
    loadChildren: () =>
      import('./contact-request/routes').then((e) => e.routes),
  },
  {
    path: 'individual-requests',
    data: {
      label: 'Individual',
      permissions: ['Files'],
    },
    loadChildren: () =>
      import('./individual-requests/routes').then((e) => e.routes),
  },
];
