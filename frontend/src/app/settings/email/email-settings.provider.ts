import { EnvironmentProviders, inject, Provider } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { EmailSettingsFacade } from './email-settings.facade';

export type EmailSettingsProvider = {
  emailSettings: EmailSettingsFacade;
  formBuilder: FormBuilder;
};

export function provideServices(): (Provider | EnvironmentProviders)[] {
  return [EmailSettingsFacade];
}

export function injectServices(): EmailSettingsProvider {
  return {
    emailSettings: inject(EmailSettingsFacade),
    formBuilder: inject(FormBuilder),
  };
}
