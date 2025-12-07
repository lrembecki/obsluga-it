import { FormFieldSchema } from '../form-field.schema';

export class NumberFormFieldSchema<T> extends FormFieldSchema<T> {
  constructor(init?: Partial<NumberFormFieldSchema<T>>) {
    super('number', init);
    Object.assign(this, init);
    this.colClass ??= 'col-3';
  }
}
