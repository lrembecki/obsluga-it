import { Type } from '@angular/core';
import { FormFieldSchema } from '../form-field.schema';

export class SelectFormFieldSchema<T> extends FormFieldSchema<T> {
  options: { label: string; value: any }[] = [];
  clearable: boolean = false;
  itemTemplate?: Type<any> = null!;
  constructor(init?: Partial<SelectFormFieldSchema<T>>) {
    super('select', init);
    Object.assign(this, init);
    this.colClass ??= 'col-3';
  }
}
