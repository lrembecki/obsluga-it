import { Routes } from '@angular/router';

export const administrationRoutes: Routes = [
  {
    path: 'permissions',
    data: { label: 'Permissions', permissions: ['Security.Permissions'] },
    loadChildren: () =>
      import('./permissions/src').then((e) => e.routes),
  },
  {
    path: 'permission-groups',
    data: { label: 'Permission Groups', permissions: ['Security.PermissionGroups'] },
    loadChildren: () =>
      import('./permission-groups/src').then((e) => e.routes),
  },
  {
    path: 'subscription-accounts',
    data: { label: 'Subscription Accounts', permissions: ['Security.AccountSubscriptions'] },
    loadChildren: () =>
      import('./subscription-accounts/src').then((e) => e.routes),
  },
];
