import { EnvironmentProviders, inject, Provider, Signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactsFacade } from './contact.facade';
import { ContactModel } from './contact.model';

export type SettingContactProvider = {
  contacts: ContactsFacade;
  router: Router;
  formBuilder: FormBuilder;
  activatedRoute: ActivatedRoute;
};

export function provideSettingContacts(): (Provider | EnvironmentProviders)[] {
  return [ContactsFacade];
}

export function injectSettingContacts(): SettingContactProvider {
  return {
    contacts: inject(ContactsFacade),
    router: inject(Router),
    formBuilder: inject(FormBuilder),
    activatedRoute: inject(ActivatedRoute),
  };
}

export function getModelFromRouteData(): Signal<ContactModel> {
  const dataSignal = toSignal(inject(ActivatedRoute).data);
  return dataSignal()!['model'] as Signal<ContactModel>;
}
