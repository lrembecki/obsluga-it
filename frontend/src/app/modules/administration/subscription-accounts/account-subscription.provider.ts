import { EnvironmentProviders, Provider, inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiFacade } from '@core/interfaces/facade.interface';
import { SecurityPermissionGroupFacade } from '../permission-groups/permission-group.provider';
import { SecurityPermissionFacade } from '../permissions/permission.provider';
import { AccountSubscriptionVM } from './account-subscription.vm';

export class SecuritySubscriptionAccountFacade extends ApiFacade<AccountSubscriptionVM[]> {
  constructor() {
    super([], 'account/subscription-accounts');
  }

  protected override withData(data: AccountSubscriptionVM[]): AccountSubscriptionVM[] {
    // Sort: default first, then active, then by email then subscription label for stability
    return [...data].sort((a, b) => {
      if (a.isDefault !== b.isDefault) return a.isDefault ? -1 : 1;
      if (a.isActive !== b.isActive) return a.isActive ? -1 : 1;
      const e = a.email.localeCompare(b.email);
      if (e !== 0) return e;
      return a.subscription.localeCompare(b.subscription);
    });
  }
}

export function provideSecuritySubscriptionAccounts(): (Provider | EnvironmentProviders)[] {
  return [
    SecuritySubscriptionAccountFacade,
    SecurityPermissionGroupFacade,
    SecurityPermissionFacade,
  ];
}

export function injectSecuritySubscriptionAccounts(): SecuritySubscriptionAccountProvider {
  return {
    subscriptions: inject(SecuritySubscriptionAccountFacade),
    groups: inject(SecurityPermissionGroupFacade),
    permissions: inject(SecurityPermissionFacade),
    router: inject(Router),
    formBuilder: inject(FormBuilder),
    activatedRoute: inject(ActivatedRoute),
  };
}

export type SecuritySubscriptionAccountProvider = {
  subscriptions: SecuritySubscriptionAccountFacade;
  groups: SecurityPermissionGroupFacade;
  permissions: SecurityPermissionFacade;
  router: Router;
  formBuilder: FormBuilder;
  activatedRoute: ActivatedRoute;
};
