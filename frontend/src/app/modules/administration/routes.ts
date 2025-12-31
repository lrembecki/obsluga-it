import { Routes } from '@angular/router';
import { featureRoute } from 'app/core/helpers/route.helper';

export const facades = {
  permissionGroups: await import(
    './permission-groups/permission-group.provider'
  ).then((e) => e.SecurityPermissionGroupFacade),
  permissions: await import('./permissions/permission.provider').then(
    (e) => e.SecurityPermissionFacade,
  ),
  subscriptionAccounts: await import(
    './subscription-accounts/account-subscription.provider'
  ).then((e) => e.SecuritySubscriptionAccountFacade),
};

export const routes: Routes = [
  featureRoute('permissions', 'Permissions', ['Security.Permissions'], () =>
    import('./permissions/routes').then((e) => e.routes),
  ),
  featureRoute(
    'permission-groups',
    'Permission Groups',
    ['Security.PermissionGroups'],
    () => import('./permission-groups/routes').then((e) => e.routes),
  ),
  featureRoute(
    'subscription-accounts',
    'Subscription Accounts',
    ['Security.AccountSubscriptions'],
    () => import('./subscription-accounts/routes').then((e) => e.routes),
  ),
];
