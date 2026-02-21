import { effect } from '@angular/core';
import { FormSchema, TextFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { FormEntryVM } from './form.vm';

export class FormsFormService extends ArrayFormService<FormEntryVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/forms/list']);
    effect(() => {
      this._schema.set(new FormSchema<FormEntryVM>({
        fields: [
          new TextFormFieldSchema<FormEntryVM>({ key: 'formDefinitionId' as any, label: 'Form Definition ID' }),
        ],
      }));
    });
  }
}
