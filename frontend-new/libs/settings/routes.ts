import { Routes } from '@angular/router';

export const settingsRoutes: Routes = [
  {
    path: 'companies',
    data: { label: 'Companies', permissions: ['Settings.Companies'] },
    loadChildren: () => import('./companies/src').then((e) => e.routes),
  },
  {
    path: 'contacts',
    data: { label: 'Contacts', permissions: ['Settings.Contacts'] },
    loadChildren: () => import('./contacts/src').then((e) => e.routes),
  },
  {
    path: 'websites',
    data: { label: 'Websites', permissions: ['Settings.Websites'] },
    loadChildren: () => import('./websites/src').then((e) => e.routes),
  },
  {
    path: 'emails',
    data: { label: 'Emails', permissions: ['Settings.Emails'] },
    loadChildren: () => import('./emails/src').then((e) => e.routes),
  },
  {
    path: 'email-templates',
    data: { label: 'Email Templates', permissions: ['Settings.EmailTemplates'] },
    loadChildren: () => import('./email-templates/src').then((e) => e.routes),
  },
  {
    path: 'form-definitions',
    data: { label: 'Form Definitions', permissions: ['Settings.FormDefinitions'] },
    loadChildren: () => import('./form-definitions/src').then((e) => e.routes),
  },
];
