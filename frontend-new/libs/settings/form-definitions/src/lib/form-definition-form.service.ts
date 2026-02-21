import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, TextareaFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { FormDefinitionVM } from './form-definition.vm';

export class SettingsFormDefinitionFormService extends ArrayFormService<FormDefinitionVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/settings/form-definitions/list']);
    effect(() => {
      this._schema.set(new FormSchema<FormDefinitionVM>({
        fields: [
          new TextFormFieldSchema<FormDefinitionVM>({ key: 'name', label: 'Name', validators: [Validators.required] }),
          new TextareaFormFieldSchema<FormDefinitionVM>({ key: 'description', label: 'Description' }),
        ],
      }));
    });
  }
}
