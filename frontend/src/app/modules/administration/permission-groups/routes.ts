import { Routes } from '@angular/router';
import { listRoute } from 'app/core/helpers/route.helper';
import { injectSecurityPermissionGroups, provideSecurityPermissionGroups, SecurityPermissionGroupProvider } from './permission-group.provider';
import { PermissionGroupVM } from './permission-group.vm';

export const routes: Routes = [
  listRoute<PermissionGroupVM, SecurityPermissionGroupProvider>(
    provideSecurityPermissionGroups(),
    (id: string, services: SecurityPermissionGroupProvider) => {
      return services.groups.data().find(e => e.id === id)!;
    },
    injectSecurityPermissionGroups,
    () => import('./permission-group-list').then(e => e.PermissionGroupList),
    () => import('./permission-group-form').then(e => e.PermissionGroupForm)
  )
];
