import { Routes } from '@angular/router';
import { featureRoute } from 'app/core/helpers/route.helper';

export const routes: Routes = [
  featureRoute('contacts', 'Contacts', ['Settings.Contacts'], () =>
    import('./contacts/routes').then((e) => e.routes),
  ),
  featureRoute('emails', 'Emails', ['Settings.Emails'], () =>
    import('./emails/routes').then((e) => e.routes),
  ),
  featureRoute(
    'form-definitions',
    'Form Definitions',
    ['Settings.FormDefinitions'],
    () => import('./form-definitions/routes').then((e) => e.routes),
  ),
];
