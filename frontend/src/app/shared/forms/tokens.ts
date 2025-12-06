import { InjectionToken } from '@angular/core';
import { ApiFacade } from '@app/core/interfaces/facade.interface';
import { FormSchema } from './schemas/form-schema';

export const FormSchemaScope = new InjectionToken<FormSchema<any>>(
  'FormSchemaScope',
);
export const FormFacadeScope = new InjectionToken<ApiFacade<any>>(
  'FormFacadeScope',
);
export const FormReturnRouteScope = new InjectionToken<string[]>(
  'FormReturnRouteScope',
);
