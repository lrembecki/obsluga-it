/* eslint-disable no-unused-vars */
import { FormFieldSchema } from '../form-field.schema';
import { FormSchemaLayout } from '../form-schema-layout.type';

export class FormSchema<T> {
  fields: FormFieldSchema<T>[] = [];
  layout: FormSchemaLayout = 'two-column';
  layoutDisabled: boolean = false;

  canDelete: (...args: any[]) => boolean = () => false;

  constructor(init?: Partial<FormSchema<T>>) {
    Object.assign(this, init);
  }
}
