import { FormFieldSchema } from '../form-field.schema';

export class TextareaFormFieldSchema<T> extends FormFieldSchema<T> {
  rows: number = 3;
  cols: number = 30;
  constructor(init?: Partial<TextareaFormFieldSchema<T>>) {
    super('textarea', init);
    Object.assign(this, init);
    this.colClass ??= 'col-12';
  }
}
