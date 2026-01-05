import { Routes } from '@angular/router';
import { featureRoute } from 'app/core/helpers/route.helper';

export const facades = {
  companies: await import('./companies/company.facade').then(
    (e) => e.SettingsCompanyFacade,
  ),
  contacts: await import('./contacts/contact.facade').then(
    (e) => e.ContactsFacade,
  ),
  websites: await import('./websites/website.facade').then(
    (e) => e.SettingsWebsiteFacade,
  ),
  emailTemplates: await import('./email-templates/email-template.facade').then(
    (e) => e.SettingsEmailTemplateFacade,
  ),
  emails: await import('./emails/email.facade').then(
    (e) => e.SettingsEmailFacade,
  ),
  formDefinitions: await import(
    './form-definitions/form-definition.facade'
  ).then((e) => e.SettingsFormDefinitionFacade),
};

export const routes: Routes = [
  featureRoute('companies', 'Companies', ['Settings.Companies'], () =>
    import('./companies/routes').then((e) => e.routes),
  ),
  featureRoute('contacts', 'Contacts', ['Settings.Contacts'], () =>
    import('./contacts/routes').then((e) => e.routes),
  ),
  featureRoute('websites', 'Websites', ['Settings.Websites'], () =>
    import('./websites/routes').then((e) => e.routes),
  ),
  featureRoute('emails', 'Emails', ['Settings.Emails'], () =>
    import('./emails/routes').then((e) => e.routes),
  ),
  featureRoute(
    'email-templates',
    'Email Templates',
    ['Settings.EmailTemplates'],
    () => import('./email-templates/routes').then((e) => e.routes),
  ),
  featureRoute(
    'form-definitions',
    'Form Definitions',
    ['Settings.FormDefinitions'],
    () => import('./form-definitions/routes').then((e) => e.routes),
  ),
];
