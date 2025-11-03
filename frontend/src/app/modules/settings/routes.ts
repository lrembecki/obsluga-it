import { Routes } from '@angular/router';
import { featureRoute } from 'app/core/helpers/route.helper';

export const routes: Routes = [
  featureRoute('email', 'Email', [], () =>
    import('./email/routes').then((e) => e.routes),
  ),
  featureRoute('contacts', 'Contacts', [], () =>
    import('./contacts/routes').then((e) => e.routes),
  ),
];
