import { inject } from '@angular/core';
import { Routes } from '@angular/router';
import { provideArrayApiFacade } from '@obsluga-it/core/facades';
import { provideDataTableService } from '@obsluga-it/shared/data-table';
import { provideFormService } from '@obsluga-it/shared/forms';
import { TrotamundosLoyalityProgramDataTableService } from './loyality-program-data-table.service';
import { TrotamundosLoyalityProgramFormService } from './loyality-program-form.service';
import { TrotamundosLoyalityProgramFacade } from './loyality-program.facade';

export const routes: Routes = [{
  path: '',
  providers: [
    provideArrayApiFacade(TrotamundosLoyalityProgramFacade),
    provideDataTableService(TrotamundosLoyalityProgramDataTableService),
    provideFormService(TrotamundosLoyalityProgramFormService),
  ],
  resolve: { _init: () => { Promise.allSettled([inject(TrotamundosLoyalityProgramFacade)].map((e) => e.initialize())); } },
  children: [
    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', loadComponent: () => import('@obsluga-it/shared/data-table').then((m) => m.DataTableTemplate) },
    { path: ':id', loadComponent: () => import('@obsluga-it/shared/forms').then((m) => m.FormTemplate) },
  ],
}];
