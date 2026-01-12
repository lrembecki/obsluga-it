import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  CheckboxFormFieldSchema,
  FormSchema,
  NumberFormFieldSchema,
  PasswordFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { ArrayFormService } from '@app/shared/forms/form.service';
import { EmailVM } from './email.vm';

export class SettingsEmailFormService extends ArrayFormService<EmailVM> {
  constructor() {
    super();

    this._returnRoute.set(['/modules/settings/emails/list']);

    effect(() => {
      this._schema.set(
        new FormSchema<EmailVM>({
          fields: [
            new CheckboxFormFieldSchema<EmailVM>({
              key: 'isActive',
              label: 'Is Active',
            }),
            new TextFormFieldSchema<EmailVM>({
              key: 'smtpServer',
              label: 'SMTP Server',
              validators: [Validators.required],
            }),
            new NumberFormFieldSchema<EmailVM>({
              key: 'smtpPort',
              label: 'SMTP Port',
              validators: [Validators.required, Validators.min(1)],
            }),
            new TextFormFieldSchema<EmailVM>({
              key: 'smtpUsername',
              label: 'SMTP Username',
              validators: [Validators.required],
            }),
            new PasswordFormFieldSchema<EmailVM>({
              key: 'smtpPassword',
              label: 'SMTP Password',
              validators: [],
            }),
            new TextFormFieldSchema<EmailVM>({
              key: 'fromAddress',
              label: 'From Address',
              validators: [Validators.required, Validators.email],
            }),
            new TextFormFieldSchema<EmailVM>({
              key: 'fromName',
              label: 'From Name',
              validators: [Validators.required],
            }),
            new TextFormFieldSchema<EmailVM>({
              key: 'replyToAddress',
              label: 'Reply-To Address',
              validators: [Validators.email],
            }),
            new TextFormFieldSchema<EmailVM>({
              key: 'replyToName',
              label: 'Reply-To Name',
            }),
          ],
        }),
      );
    });
  }
}
