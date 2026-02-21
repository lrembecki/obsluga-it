import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { ContactVM } from './contact.vm';

export class SettingsContactFormService extends ArrayFormService<ContactVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/settings/contacts/list']);

    effect(() => {
      this._schema.set(
        new FormSchema<ContactVM>({
          fields: [
            new TextFormFieldSchema<ContactVM>({ key: 'name', label: 'Name', validators: [Validators.required] }),
            new TextFormFieldSchema<ContactVM>({ key: 'email', label: 'Email', validators: [Validators.email] }),
            new TextFormFieldSchema<ContactVM>({ key: 'phone', label: 'Phone' }),
          ],
        }),
      );
    });
  }
}
