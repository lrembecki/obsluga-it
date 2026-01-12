import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  CheckboxFormFieldSchema,
  FormSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { ArrayFormService } from '@app/shared/forms/form.service';
import { ContactModel } from './contact.model';

export class SettingsContactFormService extends ArrayFormService<ContactModel> {
  constructor() {
    super();

    this._returnRoute.set(['/modules/settings/contacts/list']);

    effect(() => {
      this._schema.set(
        new FormSchema<ContactModel>({
          fields: [
            new CheckboxFormFieldSchema<ContactModel>({
              key: 'isActive',
              label: 'Is Active',
            }),
            new TextFormFieldSchema<ContactModel>({
              key: 'name',
              label: 'Name',
              validators: [Validators.required],
            }),
            new TextFormFieldSchema<ContactModel>({
              key: 'email',
              label: 'Email',
              validators: [Validators.required, Validators.email],
            }),
            new TextFormFieldSchema<ContactModel>({
              key: 'phone',
              label: 'Phone',
              validators: [Validators.required],
            }),
            new TextFormFieldSchema<ContactModel>({
              key: 'position',
              label: 'Position',
            }),
          ],
        }),
      );
    });
  }
}
