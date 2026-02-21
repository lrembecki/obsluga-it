import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, TextareaFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { EmailTemplateVM } from './email-template.vm';

export class SettingsEmailTemplateFormService extends ArrayFormService<EmailTemplateVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/settings/email-templates/list']);
    effect(() => {
      this._schema.set(new FormSchema<EmailTemplateVM>({
        fields: [
          new TextFormFieldSchema<EmailTemplateVM>({ key: 'name', label: 'Name', validators: [Validators.required] }),
          new TextFormFieldSchema<EmailTemplateVM>({ key: 'subject', label: 'Subject', validators: [Validators.required] }),
          new TextareaFormFieldSchema<EmailTemplateVM>({ key: 'body', label: 'Body', validators: [Validators.required] }),
        ],
      }));
    });
  }
}
