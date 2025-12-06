import { FormFieldSchema } from '../form-field.schema';

export class CustomFormFieldSchema<T> extends FormFieldSchema<T> {
  template: any = null!;
  constructor(init?: Partial<CustomFormFieldSchema<T>>) {
    super('custom', init);
    Object.assign(this, init);
  }
}
