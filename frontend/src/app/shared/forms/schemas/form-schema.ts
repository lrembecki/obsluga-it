/* eslint-disable no-unused-vars */
import { FormFieldSchema } from '../form-field.schema';
import { FormSchemaLayout } from '../form-schema-layout.type';

export class FormSchema<T> {
  fields: FormFieldSchema<T>[] = [];
  layout: FormSchemaLayout = 'two-column';
  layoutDisabled: boolean = false;
  patchValue: Partial<T> = {} as Partial<T>;

  canDelete: (...args: any[]) => boolean = () => true;

  constructor(init?: Partial<FormSchema<T>>) {
    Object.assign(this, init);
    this.patchValue ??= {} as Partial<T>;
  }
}
