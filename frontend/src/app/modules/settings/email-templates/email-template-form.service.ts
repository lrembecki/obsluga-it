import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  FileUploadFormFieldSchema,
  FormSchema,
  TextFormFieldSchema,
  StringCollectionFormFieldSchema,
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import { EmailTemplateVM } from './email-template.vm';

export class SettingsEmailTemplateFormService extends FormService<EmailTemplateVM> {
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
            new FileUploadFormFieldSchema<EmailTemplateVM>({
              key: 'templateHtml',
              label: 'Template HTML',
              chooseLabel: 'Upload HTML',
              colClass: 'col-6',
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
