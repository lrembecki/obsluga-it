import { FormArray } from '@angular/forms';
import { FormFieldSchema } from '../form-field.schema';
import { FormFieldType } from '../form-field.type';

export type PrimitiveCollectionItemType = Omit<
  FormFieldType,
  'group' | 'collection'
>;

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
