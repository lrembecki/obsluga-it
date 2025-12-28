export type SettingsPermissionType =
  | 'Settings.Contacts'
  | 'Settings.Emails'
  | 'Settings.FormDefinitions'
  | 'Settings.EmailTemplates';

export type AdministrationPermissionType =
  | 'Security.Permissions'
  | 'Security.AccountSubscriptions'
  | 'Security.Accounts'
  | 'Security.PermissionGroups';

export type TrotamundosPermissionType =
  | 'Trotamundos.Loyality-Program'
  | 'Trotamundos.Advantages'
  | 'Trotamundos.Highlights'
  | 'Trotamundos.Trips'
  | 'Trotamundos.Files';

export type PermissionType =
  | SettingsPermissionType
  | AdministrationPermissionType
  | TrotamundosPermissionType;
