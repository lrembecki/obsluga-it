import { Routes } from '@angular/router';
import { listRoute } from 'app/core/helpers/route.helper';
import { injectSecurityPermissions, provideSecurityPermissions, SecurityPermissionProvider } from './permission.provider';
import { PermissionVM } from './permission.vm';

export const routes: Routes = [
  listRoute<PermissionVM, SecurityPermissionProvider>(
    provideSecurityPermissions(),
    injectSecurityPermissions,
    () => import('./permission-list').then(e => e.PermissionList)
  )
];
