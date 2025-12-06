import { FormFieldSchema } from '../form-field.schema';

export class DateFormFieldSchema<T> extends FormFieldSchema<T> {
  constructor(init?: Partial<DateFormFieldSchema<T>>) {
    super('date', init);
    Object.assign(this, init);
    this.colClass ??= 'col-3';
    this.value = false;
  }
}
