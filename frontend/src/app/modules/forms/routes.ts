import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Routes } from '@angular/router';
import { provideArrayApiFacade } from '@app/core/interfaces/facade.interface';
import { provideDataTableService } from '@app/shared/data-table/data-table.service';
import { provideFormService } from '@app/shared/forms/form.service';
import { SettingsFormDefinitionFacade } from '../settings/form-definitions/form-definition.facade';
import { FormsDataTableService } from './forms-data-table.service';
import { FormsFormService } from './forms-form.service';
import { FormsFacade } from './forms.facade';
import { FormSessionService } from './forms.session';

export const routes: Routes = [
  {
    path: '',
    providers: [
      provideArrayApiFacade(FormsFacade),
      FormSessionService,
      FormsFacade,
    ],
    children: [
      {
        path: ':formDefinitionId',
        resolve: {
          _: async (state: ActivatedRouteSnapshot) => {
            const service = inject(FormSessionService);
            const formDefinitionId = state.params['formDefinitionId'];

            service.formDefinitionId.set(formDefinitionId);

            const facades = {
              formDefinitions: inject(SettingsFormDefinitionFacade),
              forms: inject(FormsFacade),
            };

            facades.forms.filter({ formDefinitionId });

            await Promise.allSettled(
              Object.values(facades).map((e) => e.initialize()),
            );
          },
        },
        children: [
          {
            path: '',
            redirectTo: 'list',
            pathMatch: 'full',
          },
          {
            path: 'list',
            data: {
              label: 'Form Entries',
            },
            providers: [provideDataTableService(FormsDataTableService)],
            loadComponent: () =>
              import('app/shared/data-table/data-table.template').then(
                (m) => m.DataTableTemplate,
              ),
          },
          {
            path: ':id',
            providers: [provideFormService(FormsFormService)],
            loadComponent: () =>
              import('app/shared/forms/form-template').then(
                (m) => m.FormTemplate,
              ),
          },
        ],
      },
    ],
  },
];
