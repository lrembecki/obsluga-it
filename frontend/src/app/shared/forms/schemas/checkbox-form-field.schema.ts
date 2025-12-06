import { FormFieldSchema } from '../form-field.schema';

export class CheckboxFormFieldSchema<T> extends FormFieldSchema<T> {
  constructor(init?: Partial<CheckboxFormFieldSchema<T>>) {
    super('checkbox', init);
    Object.assign(this, init);
    this.colClass ??= 'col-1';
  }
}
