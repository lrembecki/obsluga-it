import { FormFieldSchema } from '../form-field.schema';

export class PasswordFormFieldSchema<T> extends FormFieldSchema<T> {
  constructor(init?: Partial<PasswordFormFieldSchema<T>>) {
    super('password', init);
    Object.assign(this, init);
    this.colClass ??= 'col-3';
  }
}
