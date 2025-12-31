import { Signal, Type } from '@angular/core';
import { FormFieldSchema } from '../form-field.schema';

export class SelectFormFieldSchema<T> extends FormFieldSchema<T> {
  options: { label: string; value: any }[] = [];
  renderOptions: Signal<Array<{ label: string; value: any }>> = null!;
  multiple: boolean = false;
  clearable: boolean = false;
  itemTemplate?: Type<any> = null!;
  constructor(init?: Partial<SelectFormFieldSchema<T>>) {
    super('select', init);
    Object.assign(this, init);
    this.colClass ??= 'col-3';
  }
}
