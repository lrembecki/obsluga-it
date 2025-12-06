import { FormFieldSchema } from '../form-field.schema';

export class TextFormFieldSchema<T> extends FormFieldSchema<T> {
  constructor(init?: Partial<TextFormFieldSchema<T>>) {
    super('text', init);
    Object.assign(this, init);
    this.colClass ??= 'col-3';
  }
}
