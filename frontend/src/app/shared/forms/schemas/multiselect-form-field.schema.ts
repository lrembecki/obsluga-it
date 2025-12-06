import { Type } from '@angular/core';
import { FormControl } from '@angular/forms';
import { FormFieldSchema } from '../form-field.schema';

export class MultiSelectFormFieldSchema<T> extends FormFieldSchema<T> {
  options: { label: string; value: any }[] = [];
  clearable: boolean = false;
  itemTemplate?: Type<any> = null!;
  constructor(init?: Partial<MultiSelectFormFieldSchema<T>>) {
    super('multiselect', init);
    Object.assign(this, init);
    this.createControl = (_mode, disabled, formValue) => {
      let computed: any = undefined;
      if (this.value !== undefined) {
        computed = this.value;
      } else if (this.renderValue) {
        computed = this.renderValue(formValue as T);
      }
      const initial = Array.isArray(computed) ? computed : [];
      return new FormControl(
        { value: initial, disabled },
        this.validators ?? [],
      );
    };
    this.colClass ??= 'col-3';
  }
}
