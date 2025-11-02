import {
  computed,
  EnvironmentProviders,
  inject,
  Provider,
} from '@angular/core';
import { Router } from '@angular/router';
import { paramKeySignal } from 'app/core/helpers/route.helper';
import { IndividualRequestsFacade } from './individual-requests.facade';

export type ServicesType = {
  router: Router;
  individualRequests: IndividualRequestsFacade;
};

export function provideServices(): (Provider | EnvironmentProviders)[] {
  return [IndividualRequestsFacade];
}

export function injectServices(): ServicesType {
  return {
    router: inject(Router),
    individualRequests: inject(IndividualRequestsFacade),
  };
}

export function selectedModelComputed(services: ServicesType) {
  const params = paramKeySignal(['individualRequestId']);

  return computed(() =>
    params()
      ? services.individualRequests
          .data()
          .find((e) => e.individualRequestId == params()['individualRequestId'])
      : null,
  );
}
