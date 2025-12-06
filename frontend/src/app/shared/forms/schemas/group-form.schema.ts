import { FormFieldSchema } from '../form-field.schema';
import { createFormFromFieldSchema } from '../services/form-factory.service';

export class GroupFormFieldSchema<T, Y> extends FormFieldSchema<T> {
  nestedFields: FormFieldSchema<Y>[] = [];
  constructor(init?: Partial<GroupFormFieldSchema<T, Y>>) {
    super('group', init);
    Object.assign(this, init);
    this.createControl = (_mode, disabled) => {
      const fg = createFormFromFieldSchema({ fields: this.nestedFields || [] });
      if (disabled) fg.disable({ emitEvent: false });
      return fg;
    };
  }
}
