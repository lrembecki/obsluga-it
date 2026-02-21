import { FormControl, FormGroup, FormArray, ValidatorFn } from '@angular/forms';

export type FormFieldType =
  | 'text'
  | 'textarea'
  | 'number'
  | 'date'
  | 'time'
  | 'checkbox'
  | 'select'
  | 'multiselect'
  | 'image'
  | 'file-upload'
  | 'group'
  | 'collection'
  | 'password'
  | 'custom';

export class FormFieldSchema<T> {
  key: keyof T & string = null!;
  label: string = '';
  type: FormFieldType = 'text';
  validators: ValidatorFn[] = [];
  disabled: boolean = false;
  onChange?: (control: any, formGroup: FormGroup) => void;
  root: any = { onChange: () => {} };

  constructor(init?: Partial<FormFieldSchema<T>>) {
    Object.assign(this, init);
    if (!this.root) {
      this.root = { onChange: () => {} };
    }
  }

  createControl(mode: string, disabled: boolean = false): FormControl {
    return new FormControl({ value: null, disabled: disabled || this.disabled }, this.validators);
  }
}

export class TextFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'text';
  placeholder?: string;

  constructor(init?: Partial<TextFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }
}

export class TextareaFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'textarea';
  rows?: number = 3;

  constructor(init?: Partial<TextareaFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }
}

export class NumberFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'number';
  min?: number;
  max?: number;

  constructor(init?: Partial<NumberFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }
}

export class DateFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'date';

  constructor(init?: Partial<DateFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }
}

export class CheckboxFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'checkbox';

  constructor(init?: Partial<CheckboxFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }
}

export class SelectFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'select';
  options: Array<{ label: string; value: unknown }> = [];

  constructor(init?: Partial<SelectFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }
}

export class MultiSelectFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'multiselect';
  options: Array<{ label: string; value: unknown }> = [];

  constructor(init?: Partial<MultiSelectFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }
}

export class ImageFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'image';

  constructor(init?: Partial<ImageFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }
}

export class FileUploadFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'file-upload';

  constructor(init?: Partial<FileUploadFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }
}

export class GroupFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'group';
  fields: FormFieldSchema<any>[] = [];

  constructor(init?: Partial<GroupFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }

  override createControl(): any {
    const group: Record<string, FormControl> = {};
    for (const field of this.fields) {
      group[field.key as string] = field.createControl('edit', field.disabled);
    }
    return new FormGroup(group);
  }
}

export class CollectionFormFieldSchema<T> extends FormFieldSchema<T> {
  override type: FormFieldType = 'collection';
  itemFields: FormFieldSchema<any>[] = [];

  constructor(init?: Partial<CollectionFormFieldSchema<T>>) {
    super(init);
    Object.assign(this, init);
  }

  override createControl(): any {
    return new FormArray([]);
  }
}

export class FormSchema<T> {
  fields: FormFieldSchema<T>[] = [];
  canDelete: (value: any) => boolean = () => true;

  constructor(init?: Partial<FormSchema<T>>) {
    Object.assign(this, init);
  }

  buildFormGroup(): FormGroup {
    const controls: Record<string, any> = {};
    for (const field of this.fields) {
      controls[field.key as string] = field.createControl('edit', field.disabled);
    }
    return new FormGroup(controls);
  }
}
