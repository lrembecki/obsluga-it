import { effect, inject } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  FileUploadFormFieldSchema,
  FormSchema,
  MultiSelectFormFieldSchema,
  StringCollectionFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import { ContactsFacade } from '../contacts/contact.facade';
import { EmailTemplateVM } from './email-template.vm';
export class SettingsEmailTemplateFormService extends FormService<EmailTemplateVM> {
  private readonly _facades = {
    contacts: inject(ContactsFacade),
  };

  constructor() {
    super();

    this._returnRoute.set(['/modules/settings/email-templates/list']);

    effect(() => {
      this._schema.set(
        new FormSchema<EmailTemplateVM>({
          fields: [
            new TextFormFieldSchema<EmailTemplateVM>({
              key: 'name',
              label: 'Name',
              validators: [Validators.required],
              colClass: 'col-6',
            }),
            new TextFormFieldSchema<EmailTemplateVM>({
              key: 'subject',
              label: 'Subject',
              validators: [Validators.required],
              colClass: 'col-6',
            }),
            new FileUploadFormFieldSchema<EmailTemplateVM>({
              key: 'templateHtml',
              label: 'Template HTML',
              chooseLabel: 'Upload HTML',
              colClass: 'col-6',
            }),
            new MultiSelectFormFieldSchema<EmailTemplateVM>({
              key: 'contacts_cc',
              label: 'CC Contacts',
              options: this._facades.contacts.data().map((c) => ({
                label: `${c.name} (${c.email})`,
                value: c.id,
              })),
              colClass: 'col-4',
            }),
            new MultiSelectFormFieldSchema<EmailTemplateVM>({
              key: 'contacts_bcc',
              label: 'BCC Contacts',
              options: this._facades.contacts.data().map((c) => ({
                label: `${c.name} (${c.email})`,
                value: c.id,
              })),
              colClass: 'col-4',
            }),
            new MultiSelectFormFieldSchema<EmailTemplateVM>({
              key: 'contacts_to',
              label: 'To Contacts',
              options: this._facades.contacts.data().map((c) => ({
                label: `${c.name} (${c.email})`,
                value: c.id,
              })),
              colClass: 'col-4',
            }),
            new StringCollectionFormFieldSchema<EmailTemplateVM>({
              key: 'fields',
              label: 'Fields',
              layout: 'vertical',
              addButtonText: 'Add Field',
              emptyText: 'No fields',
            }),
          ],
        }),
      );
    });
  }
}
