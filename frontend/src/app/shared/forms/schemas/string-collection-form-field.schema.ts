import { CollectionFormFieldSchema } from './collection-form-field.schema';

// Convenience schema for collections of primitive strings
export class StringCollectionFormFieldSchema<T> extends CollectionFormFieldSchema<T, string> {
  constructor(init?: Partial<StringCollectionFormFieldSchema<T>>) {
    super({
      itemType: 'text',
      layout: 'vertical',
      addButtonText: 'Add Item',
      emptyText: 'No items',
      ...init,
    } as any);
    Object.assign(this, init);
  }
}
