import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideObjectApiFacade } from '@app/core/interfaces/facade.interface';
import {
  FormTemplateModelProvider,
  ObjectFormTemplateModelProvider,
} from '@app/shared/forms/form-model.provider';
import { provideFormService } from '@app/shared/forms/form.service';
import { TrotamundosIndividualTripFormService } from './individual-trip-form.service';
import { TrotamundosIndividualTripFacade } from './individual-trip.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [provideObjectApiFacade(TrotamundosIndividualTripFacade)],
    resolve: {
      _init: () =>
        Promise.allSettled(
          [inject(TrotamundosIndividualTripFacade)].map((ArrayFacade) =>
            ArrayFacade.initialize(),
          ),
        ),
    },
    children: [
      {
        path: '',
        providers: [
          provideFormService(TrotamundosIndividualTripFormService),
          ObjectFormTemplateModelProvider,
          {
            provide: FormTemplateModelProvider,
            useExisting: ObjectFormTemplateModelProvider,
          },
        ],
        loadComponent: () =>
          import('app/shared/forms/form-template').then((m) => m.FormTemplate),
      },
    ],
  },
];
