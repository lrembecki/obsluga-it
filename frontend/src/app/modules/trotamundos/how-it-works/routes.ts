import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideObjectApiFacade } from '@app/core/interfaces/facade.interface';
import {
  FormTemplateModelProvider,
  ObjectFormTemplateModelProvider,
} from '@app/shared/forms/form-model.provider';
import { provideFormService } from '@app/shared/forms/form.service';
import { TrotamundosHowItWorksFormService } from './how-it-works-form.service';
import { TrotamundosHowItWorksFacade } from './how-it-works.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [provideObjectApiFacade(TrotamundosHowItWorksFacade)],
    resolve: {
      _init: () =>
        Promise.allSettled(
          [inject(TrotamundosHowItWorksFacade)].map((ArrayFacade) =>
            ArrayFacade.initialize(),
          ),
        ),
    },
    children: [
      {
        path: '',
        providers: [
          provideFormService(TrotamundosHowItWorksFormService),
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
