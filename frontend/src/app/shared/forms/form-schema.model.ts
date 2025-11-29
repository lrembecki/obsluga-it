import { InjectionToken, Type } from '@angular/core';
import { ApiFacade } from '@app/core/interfaces/facade.interface';

export type FormFieldType =
  | 'text'
  | 'textarea'
  | 'select'
  | 'checkbox'
  | 'number'
  | 'date';
export type FormSchemaLayout = 'single-column' | 'two-column';

export const FormSchemaScope = new InjectionToken<FormSchema<any>>(
  'FormSchemaScope',
);
export const FormFacadeScope = new InjectionToken<ApiFacade<any>>(
  'FormFacadeScope',
);
export const FormReturnRouteScope = new InjectionToken<string[]>(
  'FormReturnRouteScope',
);

export class FormFieldSchema<T> {
  key: keyof T = null!;
  label: string = null!;
  type: FormFieldType = null!;
  placeholder: string = null!;
  validators: any[] = [];
  disabled: boolean = false;
  disabledOnEdit?: boolean = false;
  disabledOnCreate?: boolean = false;
  // eslint-disable-next-line no-unused-vars
  dynamicDisabled?: (formValue: T) => boolean = (_v) => false;

  constructor(type: FormFieldType, init?: Partial<FormFieldSchema<T>>) {
    Object.assign(this, init);
    this.type = type;
  }
}

export class TextFormFieldSchema<T> extends FormFieldSchema<T> {
  constructor(init?: Partial<TextFormFieldSchema<T>>) {
    super('text', init);
    Object.assign(this, init);
  }
}

export class TextareaFormFieldSchema<T> extends FormFieldSchema<T> {
  rows: number = 3;
  cols: number = 30;
  constructor(init?: Partial<TextareaFormFieldSchema<T>>) {
    super('textarea', init);
    Object.assign(this, init);
  }
}

export class CheckboxFormFieldSchema<T> extends FormFieldSchema<T> {
  constructor(init?: Partial<CheckboxFormFieldSchema<T>>) {
    super('checkbox', init);
    Object.assign(this, init);
  }
}

export class SelectFormFieldSchema<T> extends FormFieldSchema<T> {
  options: { label: string; value: any }[] = [];
  clearable: boolean = false;
  itemTemplate?: Type<any> = null!;
  constructor(init?: Partial<SelectFormFieldSchema<T>>) {
    super('select', init);
    Object.assign(this, init);
  }
}

export class FormSchema<T> {
  fields: FormFieldSchema<T>[] = [];
  layout: FormSchemaLayout = 'two-column';
  layoutDisabled: boolean = false;
  // eslint-disable-next-line no-unused-vars
  canDelete: (data: any) => boolean = (_data) => false;

  constructor(init?: Partial<FormSchema<T>>) {
    Object.assign(this, init);
  }
}
