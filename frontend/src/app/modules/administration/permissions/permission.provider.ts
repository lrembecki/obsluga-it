import { EnvironmentProviders, inject, Provider } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ArrayApiFacade } from 'app/core/interfaces/facade.interface';
import { PermissionVM } from './permission.vm';

export class SecurityPermissionFacade extends ArrayApiFacade<PermissionVM> {
  constructor() {
    super([], 'account/permissions');
  }

  protected override withData(data: PermissionVM[]): PermissionVM[] {
    return data.sort((a, b) => a.name.localeCompare(b.name));
  }
}

export function provideSecurityPermissions(): (
  | Provider
  | EnvironmentProviders
)[] {
  return [SecurityPermissionFacade];
}

export function injectSecurityPermissions(): SecurityPermissionProvider {
  return {
    permissions: inject(SecurityPermissionFacade),
    router: inject(Router),
    formBuilder: inject(FormBuilder),
    activatedRoute: inject(ActivatedRoute),
  };
}

export type SecurityPermissionProvider = {
  permissions: SecurityPermissionFacade;
  router: Router;
  formBuilder: FormBuilder;
  activatedRoute: ActivatedRoute;
};
