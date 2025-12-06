import { inject } from '@angular/core';
import { FormSchema } from '@app/shared/forms';
import { routeFeatureTemplate } from 'app/core/helpers/route.helper';
import {
  provideSecurityPermissionGroups,
  SecurityPermissionGroupFacade,
} from './permission-group.provider';
import { PermissionGroupVM } from './permission-group.vm';

export const routes = routeFeatureTemplate(
  [provideSecurityPermissionGroups()],
  () => ({
    facade: inject(SecurityPermissionGroupFacade),
    columns: [{ field: 'name', label: 'Name', type: 'text', sortable: true }],
    persistenceKey: 'security.permission-groups.list',
  }),
  () => new FormSchema<PermissionGroupVM>(),
);
