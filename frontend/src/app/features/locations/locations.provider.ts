import {
  computed,
  EnvironmentProviders,
  inject,
  Provider,
} from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ImageFacade } from 'app/core/facades/image.facade';
import { paramKeySignal } from 'app/core/helpers/route.helper';
import { LocationsFacade } from './locations.facade';

export type ServiceType = {
  locations: LocationsFacade;
  images: ImageFacade;
  router: Router;
  formBuilder: FormBuilder;
};

export function provideLocations(): (Provider | EnvironmentProviders)[] {
  return [LocationsFacade];
}

export function injectLocations(): ServiceType {
  return {
    locations: inject(LocationsFacade),
    images: inject(ImageFacade),
    router: inject(Router),
    formBuilder: inject(FormBuilder),
  };
}

export function selectedModelComputed(services: ServiceType) {
  const params = paramKeySignal(['locationId']);

  return computed(() =>
    params()
      ? services.locations
          .data()
          .find((e) => e.locationId == params()['locationId'])
      : null,
  );
}

export function formGroupComputed(services: ServiceType) {
  const model = selectedModelComputed(services);
  const formBuilder = inject(FormBuilder);
  return computed(() =>
    formBuilder.group({
      locationId: [model()?.locationId ?? null!, []],
      name: [model()?.name ?? null!, [Validators.required]],
      description: [model()?.description ?? null!, []],
    }),
  );
}
