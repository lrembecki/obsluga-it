/* eslint-disable no-unused-vars */
import { AbstractControl, FormControl } from '@angular/forms';
import { FormFieldType } from './form-field.type';

export class FormFieldSchema<T> {
  key: keyof T = null!;
  label: string = null!;
  type: FormFieldType = null!;
  placeholder: string = null!;
  validators: any[] = [];
  disabled: boolean = false;
  disabledOnEdit?: boolean = false;
  disabledOnCreate?: boolean = false;
  // Controls visual visibility of the field in the renderer
  isVisible: boolean = true;
  // Optional grid column class for layout: 'col-1' ... 'col-12'
  colClass?: string;

  // Optional static value to pass to the renderer (for read-only or computed displays)
  value?: any;
  // Optional renderer function to derive a display value from the current form value
  // Return any type suitable for the target control (string, number, boolean, Date, object)
  renderValue?: (formValue: T) => any;

  dynamicDisabled?: (formValue?: T) => boolean = () => false;
  createControl: (
    mode: 'create' | 'edit',
    disabled: boolean,
    formValue?: T,
  ) => AbstractControl = (_mode, disabled, formValue) => {
    let initial: any = null;
    if (this.value !== undefined) {
      initial = this.value;
    } else if (this.renderValue) {
      initial = this.renderValue(formValue as T);
    }
    return new FormControl({ value: initial, disabled }, this.validators ?? []);
  };

  constructor(type: FormFieldType, init?: Partial<FormFieldSchema<T>>) {
    Object.assign(this, init);
    this.type = type;
  }
}
