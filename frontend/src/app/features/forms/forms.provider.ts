import {
  computed,
  EnvironmentProviders,
  inject,
  Provider,
} from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute } from '@angular/router';
import { paramKeySignal } from 'app/core/helpers/route.helper';
import { FormsFacade } from './forms.facade';
export function provideForms(): (Provider | EnvironmentProviders)[] {
  return [FormsFacade];
}

export function injectForms() {
  return {
    forms: inject(FormsFacade),
  };
}

export function selectedFormComputed() {
  const services = injectForms();

  const params = paramKeySignal(['formId'], 1);
  return computed(() =>
    params()
      ? services.forms.data().find((e) => e.formId == params()['formId'])
      : null,
  );
}

export function selectedFormControlComputed() {
  const model = selectedFormComputed();
  const params = toSignal(inject(ActivatedRoute)!.params);
  const formControlId = computed(() =>
    params() ? params()!['formControlId'] : null,
  );

  return computed(() =>
    model()?.controls.find((e) => e.formControlId === formControlId()),
  );
}
