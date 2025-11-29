import { Routes } from '@angular/router';
import { featureRoute } from 'app/core/helpers/route.helper';

export const routes: Routes = [
  featureRoute('advantages', 'Advantages', ['Trotamundos.Advantages'], () =>
    import('./advantages/routes').then((e) => e.routes),
  ),
  featureRoute('highlights', 'Highlights', ['Trotamundos.Highlights'], () =>
    import('./highlights/routes').then((e) => e.routes),
  ),
  featureRoute(
    'loyality-program',
    'Loyality Program',
    ['Trotamundos.Loyality-Program'],
    () => import('./loyality-program/routes').then((e) => e.routes),
  ),
  featureRoute('trips', 'Trips', ['Trotamundos.Trips'], () =>
    import('./trips/routes').then((e) => e.routes),
  ),
  featureRoute('files', 'Files', ['Trotamundos.Files'], () =>
    import('./files/routes').then((e) => e.routes),
  ),
];
