export type SettingsPermissionType =
  | 'Settings.Contacts'
  | 'Settings.Emails'
  | 'Settings.FormDefinitions'
  | 'Settings.EmailTemplates'
  | 'Settings.Websites';

export type AdministrationPermissionType =
  | 'Security.Permissions'
  | 'Security.AccountSubscriptions'
  | 'Security.Accounts'
  | 'Security.PermissionGroups'
  | 'Settings.Companies';

export type TrotamundosPermissionType =
  | 'Trotamundos.Loyality-Program'
  | 'Trotamundos.Advantages'
  | 'Trotamundos.Highlights'
  | 'Trotamundos.Trips'
  | 'Trotamundos.Files'
  | 'Trotamundos.Pages.AboutUs';

export type PermissionType =
  | SettingsPermissionType
  | AdministrationPermissionType
  | TrotamundosPermissionType;
