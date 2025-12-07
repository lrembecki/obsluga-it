import { Routes } from '@angular/router';
import { featureRoute } from 'app/core/helpers/route.helper';

export const routes: Routes = [
  featureRoute('contacts', 'Contacts', [], () =>
    import('./contacts/routes').then((e) => e.routes),
  ),
  featureRoute('emails', 'Emails', [], () =>
    import('./emails/routes').then((e) => e.routes),
  ),
];
