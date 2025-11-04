import { Routes } from '@angular/router';
import { featureRoute } from 'app/core/helpers/route.helper';

export const routes: Routes = [
  featureRoute('permissions', 'Permissions', [], () =>
    import('./permissions/routes').then((e) => e.routes),
  ),
  featureRoute('permission-groups', 'Permission Groups', [], () =>
    import('./permission-groups/routes').then((e) => e.routes),
  ),
];
