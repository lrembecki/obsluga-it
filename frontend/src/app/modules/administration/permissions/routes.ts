import { Routes } from '@angular/router';
import { listRoute } from 'app/core/helpers/route.helper';
import { injectSecurityPermissions, provideSecurityPermissions, SecurityPermissionProvider } from './permission.provider';
import { PermissionVM } from './permission.vm';

export const routes: Routes = [
  listRoute<PermissionVM, SecurityPermissionProvider>(
    provideSecurityPermissions(),
    (id: string, services: SecurityPermissionProvider) => {
      return services.permissions.data().find(e => e.id === id)!;
    },
    injectSecurityPermissions,
    () => import('./permission-list').then(e => e.PermissionList)
  )
];
