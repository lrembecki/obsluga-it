import {
  computed,
  EnvironmentProviders,
  inject,
  Provider,
} from '@angular/core';
import { Router } from '@angular/router';
import { paramKeySignal } from 'app/core/helpers/route.helper';
import { ContactRequestFacade } from './contact-request.facade';

export type ServicesType = {
  contacts: ContactRequestFacade;
  router: Router;
};

export function provideServices(): (Provider | EnvironmentProviders)[] {
  return [ContactRequestFacade];
}

export function injectServices(): ServicesType {
  return {
    contacts: inject(ContactRequestFacade),
    router: inject(Router),
  };
}

export function selectedModelComputed(services: ServicesType) {
  const params = paramKeySignal(['contactRequestId']);

  return computed(() =>
    params()
      ? services.contacts
          .data()
          .find((e) => e.contactRequestId == params()['contactRequestId'])
      : null,
  );
}
