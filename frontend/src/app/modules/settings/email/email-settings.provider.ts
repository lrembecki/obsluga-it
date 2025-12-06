import { EnvironmentProviders, inject, Provider } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import {
  FormFacadeScope,
  FormReturnRouteScope,
  FormSchemaScope,
} from '@app/shared/forms';
import { emailFormSchema } from './email-form.schema';
import { EmailSettingsFacade } from './email-settings.facade';

export type EmailSettingsProvider = {
  emailSettings: EmailSettingsFacade;
  formBuilder: FormBuilder;
};

export function provideServices(): (Provider | EnvironmentProviders)[] {
  return [
    EmailSettingsFacade,
    { provide: FormFacadeScope, useExisting: EmailSettingsFacade },
    { provide: FormSchemaScope, useValue: emailFormSchema },
    { provide: FormReturnRouteScope, useValue: null! },
  ];
}

export function injectServices(): EmailSettingsProvider {
  return {
    emailSettings: inject(EmailSettingsFacade),
    formBuilder: inject(FormBuilder),
  };
}
