import { Routes } from '@angular/router';

import {
  routeFeature,
  routeForm,
  routeList,
} from '@app/core/helpers/route.helper';
import { FormSchema } from '@app/shared/forms/form-schema.model';
import { fileListSchema } from './file-list.schema';
import { TrotamundosFileFacade } from './file.provider';
import { FileVM } from './file.vm';

let featureRoute = routeFeature(TrotamundosFileFacade);
featureRoute = routeList([TrotamundosFileFacade], fileListSchema, featureRoute);
featureRoute = routeForm(
  [TrotamundosFileFacade],
  () =>
    new FormSchema<FileVM>({
      layout: 'two-column',
      fields: [],
    }),
  featureRoute,
);

export const routes: Routes = [featureRoute];
