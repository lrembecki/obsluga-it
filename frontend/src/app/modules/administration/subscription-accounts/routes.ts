import { Routes } from '@angular/router';
import { listRoute } from 'app/core/helpers/route.helper';
import { injectSecuritySubscriptionAccounts, provideSecuritySubscriptionAccounts, SecuritySubscriptionAccountProvider } from './account-subscription.provider';
import { AccountSubscriptionVM } from './account-subscription.vm';

export const routes: Routes = [
  listRoute<AccountSubscriptionVM, SecuritySubscriptionAccountProvider>(
    provideSecuritySubscriptionAccounts(),
    (id: string, services: SecuritySubscriptionAccountProvider) => services.subscriptions.data().find(e => e.id === id)!,
    injectSecuritySubscriptionAccounts,
    () => import('./account-subscription-list').then(m => m.AccountSubscriptionList),
    () => import('./account-subscription-form').then(m => m.AccountSubscriptionForm)
  )
];
