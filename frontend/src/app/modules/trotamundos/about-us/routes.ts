import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideObjectApiFacade } from '@app/core/interfaces/facade.interface';
import {
  FormTemplateModelProvider,
  ObjectFormTemplateModelProvider,
} from '@app/shared/forms/form-model.provider';
import { provideFormService } from '@app/shared/forms/form.service';
import { TrotamundosAboutUsFormService } from './about-us-form.service';
import { TrotamundosAboutUsFacade } from './about-us.facade';

export const routes: Routes = [
  {
    path: '',
    providers: [provideObjectApiFacade(TrotamundosAboutUsFacade)],
    resolve: {
      _init: () =>
        Promise.allSettled(
          [inject(TrotamundosAboutUsFacade)].map((ArrayFacade) =>
            ArrayFacade.initialize(),
          ),
        ),
    },
    children: [
      {
        path: '',
        providers: [
          provideFormService(TrotamundosAboutUsFormService),
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
