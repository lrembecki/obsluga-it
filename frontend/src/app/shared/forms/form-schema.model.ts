/* eslint-disable no-unused-vars */
import { InjectionToken, Type } from '@angular/core';
import { AbstractControl, FormArray, FormControl } from '@angular/forms';
import { ApiFacade } from '@app/core/interfaces/facade.interface';

export type FormFieldType =
  | 'text'
  | 'textarea'
  | 'select'
  | 'multiselect'
  | 'checkbox'
  | 'number'
  | 'date'
  | 'image'
  | 'custom'
  | 'collection';
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
  // Controls visual visibility of the field in the renderer
  isVisible: boolean = true;
  // Optional grid column class for layout: 'col-1' ... 'col-12'
  colClass?: string;

  dynamicDisabled?: (formValue: T) => boolean = (_v) => false;
  createControl: (
    mode: 'create' | 'edit',
    disabled: boolean,
  ) => AbstractControl = (_mode, disabled) =>
    new FormControl({ value: null, disabled }, this.validators ?? []);

  constructor(type: FormFieldType, init?: Partial<FormFieldSchema<T>>) {
    Object.assign(this, init);
    this.type = type;
  }
}

export class TextFormFieldSchema<T> extends FormFieldSchema<T> {
  constructor(init?: Partial<TextFormFieldSchema<T>>) {
    super('text', init);
    Object.assign(this, init);
    this.colClass ??= 'col-3';
  }
}

export class TextareaFormFieldSchema<T> extends FormFieldSchema<T> {
  rows: number = 3;
  cols: number = 30;
  constructor(init?: Partial<TextareaFormFieldSchema<T>>) {
    super('textarea', init);
    Object.assign(this, init);
    this.colClass ??= 'col-12';
  }
}

export class CheckboxFormFieldSchema<T> extends FormFieldSchema<T> {
  constructor(init?: Partial<CheckboxFormFieldSchema<T>>) {
    super('checkbox', init);
    Object.assign(this, init);
    this.colClass ??= 'col-1';
  }
}

export class DateFormFieldSchema<T> extends FormFieldSchema<T> {
  constructor(init?: Partial<DateFormFieldSchema<T>>) {
    super('date', init);
    Object.assign(this, init);
    this.colClass ??= 'col-3';
  }
}

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

export class MultiSelectFormFieldSchema<T> extends FormFieldSchema<T> {
  options: { label: string; value: any }[] = [];
  clearable: boolean = false;
  itemTemplate?: Type<any> = null!;
  constructor(init?: Partial<MultiSelectFormFieldSchema<T>>) {
    super('multiselect', init);
    Object.assign(this, init);
    this.createControl = (_mode, disabled) =>
      new FormControl({ value: [], disabled }, this.validators ?? []);
    this.colClass ??= 'col-3';
  }
}

export class ImageFormFieldSchema<T> extends FormFieldSchema<T> {
  // Accept only image MIME types
  accept: string = 'image/*';
  // Show image preview
  showPreview: boolean = true;
  constructor(init?: Partial<ImageFormFieldSchema<T>>) {
    super('image', init);
    Object.assign(this, init);
    this.colClass ??= 'col-6';
  }
}

export class CustomFormFieldSchema<T> extends FormFieldSchema<T> {
  template: any = null!;
  constructor(init?: Partial<CustomFormFieldSchema<T>>) {
    super('custom', init);
    Object.assign(this, init);
  }
}

export type PrimitiveCollectionItemType =
  | 'text'
  | 'textarea'
  | 'select'
  | 'checkbox'
  | 'number'
  | 'date';

export class CollectionFormFieldSchema<
  T,
  I = unknown,
> extends FormFieldSchema<T> {
  layout: 'vertical' | 'horizontal' = 'vertical';
  // Primitive item type when collection holds primitives
  itemType?: PrimitiveCollectionItemType;
  // Nested schema when collection holds objects
  itemFields?: FormFieldSchema<I>[];
  // Optional field name to keep 1-based order in items
  orderField?: keyof I;
  // UX options
  addButtonText?: string;
  emptyText?: string;
  minItems?: number;
  maxItems?: number;
  // Default column class applied to all nested item fields unless individual field.colClass is set
  itemColClass?: string;

  constructor(init?: Partial<CollectionFormFieldSchema<T, I>>) {
    super('collection', init);
    Object.assign(this, init);
    this.createControl = (_mode, disabled) => {
      const fa = new FormArray([]);
      if (disabled) fa.disable({ emitEvent: false });
      return fa;
    };
  }
}

export class FormSchema<T> {
  fields: FormFieldSchema<T>[] = [];
  layout: FormSchemaLayout = 'two-column';
  layoutDisabled: boolean = false;

  canDelete: (data: any) => boolean = (_data) => false;

  constructor(init?: Partial<FormSchema<T>>) {
    Object.assign(this, init);
  }
}
