import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { EmailVM } from './email.vm';

export class SettingsEmailFormService extends ArrayFormService<EmailVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/settings/emails/list']);
    effect(() => {
      this._schema.set(new FormSchema<EmailVM>({
        fields: [
          new TextFormFieldSchema<EmailVM>({ key: 'name', label: 'Name', validators: [Validators.required] }),
          new TextFormFieldSchema<EmailVM>({ key: 'address', label: 'Address', validators: [Validators.required, Validators.email] }),
        ],
      }));
    });
  }
}
