/* eslint-disable no-unused-vars */
import { FormGroup } from '@angular/forms';
import { FormFieldSchema } from '../form-field.schema';
import { FormSchemaLayout } from '../form-schema-layout.type';
import { CollectionFormFieldSchema } from './collection-form-field.schema';
import { GroupFormFieldSchema } from './group-form.schema';
import { StringCollectionFormFieldSchema } from './string-collection-form-field.schema';

export class FormSchema<T> {
  fields: FormFieldSchema<T>[] = [];
  layout: FormSchemaLayout = 'two-column';
  layoutDisabled: boolean = false;
  patchValue: Partial<T> = {} as Partial<T>;

  onChange: (formGroup: FormGroup) => void = () => {};
  canDelete: (...args: any[]) => boolean = () => true;

  constructor(init?: Partial<FormSchema<T>>) {
    Object.assign(this, init);
    this.patchValue ??= {} as Partial<T>;

    this.assignRoot(this.fields);
  }

  private assignRoot(fields: FormFieldSchema<any>[]) {
    fields.forEach((e) => {
      e.root = this;
      if (e instanceof GroupFormFieldSchema) {
        this.assignRoot(e.nestedFields);
      } else if (e instanceof CollectionFormFieldSchema) {
        this.assignRoot(e.itemFields!);
      } else if (e instanceof StringCollectionFormFieldSchema) {
        this.assignRoot(e.itemFields!);
      }
    });
  }
}
