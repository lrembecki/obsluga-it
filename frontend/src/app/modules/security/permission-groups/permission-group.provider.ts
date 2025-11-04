import { EnvironmentProviders, inject, Provider } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { SecurityPermissionFacade } from '../permissions/permission.provider';
import { PermissionGroupVM } from './permission-group.vm';

export class SecurityPermissionGroupFacade extends ApiFacade<PermissionGroupVM[]> {
  constructor() {
    super([], 'account/permission-groups');
  }
}

export function provideSecurityPermissionGroups(): (Provider | EnvironmentProviders)[] {
  return [SecurityPermissionGroupFacade, SecurityPermissionFacade];
}

export function injectSecurityPermissionGroups(): SecurityPermissionGroupProvider {
  return {
    groups: inject(SecurityPermissionGroupFacade),
    permissions: inject(SecurityPermissionFacade),
    router: inject(Router),
    formBuilder: inject(FormBuilder),
    activatedRoute: inject(ActivatedRoute),
  };
}

export type SecurityPermissionGroupProvider = {
  groups: SecurityPermissionGroupFacade;
  permissions: SecurityPermissionFacade;
  router: Router;
  formBuilder: FormBuilder;
  activatedRoute: ActivatedRoute;
};
