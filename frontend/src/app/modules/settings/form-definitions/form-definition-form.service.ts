import { effect, inject } from '@angular/core';
import { FormArray, Validators } from '@angular/forms';
import {
  CheckboxFormFieldSchema,
  CollectionFormFieldSchema,
  FormSchema,
  GroupFormFieldSchema,
  SelectFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import { SettingsEmailTemplateFacade } from '../email-templates/email-template.facade';
import { EmailTemplateVM } from '../email-templates/email-template.vm';
import { SettingsEmailFacade } from '../emails/email.facade';
import {
  FormDefinitionFieldType,
  FormDefinitionVM,
  FormFieldDefinitionVM,
} from './form-definition.vm';

const fieldTypeOptions: Array<{
  label: string;
  value: FormDefinitionFieldType;
}> = [
  { label: 'Text', value: 'text' },
  { label: 'Email', value: 'email' },
  { label: 'Number', value: 'number' },
  { label: 'Date', value: 'date' },
  { label: 'DateTime', value: 'date-time' },
  { label: 'Textarea', value: 'textarea' },
  { label: 'Select', value: 'select' },
  { label: 'Checkbox', value: 'checkbox' },
  { label: 'Radio', value: 'radio' },
];

export class SettingsFormDefinitionFormService extends FormService<FormDefinitionVM> {
  private readonly facades = {
    emailTemplates: inject(SettingsEmailTemplateFacade),
    emails: inject(SettingsEmailFacade),
  };

  private _selectedEmailTemplate: EmailTemplateVM | null = null;
  private _fields: FormFieldDefinitionVM[] = [];

  constructor() {
    super();

    this._returnRoute.set(['/modules/settings/form-definitions/list']);

    effect(() => {
      const model = this.model();
      if (model?.notification?.email?.emailTemplateId) {
        const selectedTemplate =
          this.facades.emailTemplates
            .data()
            .find(
              (et) => et.id === model.notification?.email?.emailTemplateId,
            ) ?? null;
        this._selectedEmailTemplate = selectedTemplate;
      }

      this._fields = model?.fields || [];

      this._schema.set(
        new FormSchema<FormDefinitionVM>({
          fields: [
            new TextFormFieldSchema<FormDefinitionVM>({
              key: 'name',
              label: 'Name',
              validators: [Validators.required],
            }),
            new CollectionFormFieldSchema<
              FormDefinitionVM,
              FormFieldDefinitionVM
            >({
              key: 'fields',
              label: 'Fields',
              layout: 'vertical',
              itemColClass: 'col-3',
              emptyText: 'No fields',
              onChange: (control) => {
                const currentFields =
                  control instanceof FormArray
                    ? (control.getRawValue() as FormFieldDefinitionVM[])
                    : (control?.value as FormFieldDefinitionVM[]);
                this._fields = currentFields ?? [];
              },
              itemFields: [
                new TextFormFieldSchema<FormFieldDefinitionVM>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-2',
                }),
                new TextFormFieldSchema<FormFieldDefinitionVM>({
                  key: 'fieldName',
                  label: 'Field Name',
                  validators: [Validators.required],
                  colClass: 'col-4',
                }),
                new SelectFormFieldSchema<FormFieldDefinitionVM>({
                  key: 'fieldType',
                  label: 'Field Type',
                  options: fieldTypeOptions,
                  validators: [Validators.required],
                  colClass: 'col-4',
                }),
                new CheckboxFormFieldSchema<FormFieldDefinitionVM>({
                  key: 'isRequired',
                  label: 'Required',
                  colClass: 'col-2',
                  value: true,
                }),
              ],
            }),
            new CollectionFormFieldSchema<FormDefinitionVM, any>({
              key: 'emailNotificationFieldMapping',
              itemFields: [
                new SelectFormFieldSchema<any>({
                  key: 'emailTemplateFieldName',
                  label: 'Email Template Field Name',
                  options: (this._selectedEmailTemplate?.fields || [])
                    .map((f) => ({ label: f, value: f }))
                    .concat([
                      { label: '@MAIL_TO', value: '@MAIL_TO' },
                      { label: '@MAIL_BCC', value: '@MAIL_BCC' },
                      { label: '@MAIL_SUBJECT', value: '@MAIL_SUBJECT' },
                      { label: '@MAIL_CC', value: '@MAIL_CC' },
                    ]),
                  validators: [Validators.required],
                  colClass: 'col-6',
                }),
                new SelectFormFieldSchema<any>({
                  key: 'formDefinitionFieldName',
                  label: 'Form Definition Field Name',
                  options: (this._fields || []).map((f) => ({
                    label: f.fieldName,
                    value: f.fieldName,
                  })),
                  validators: [Validators.required],
                  colClass: 'col-6',
                }),
              ],
            }),
            new GroupFormFieldSchema<FormDefinitionVM, any>({
              key: 'notification',
              label: 'Notification',
              nestedFields: [
                new GroupFormFieldSchema<any, any>({
                  key: 'email',
                  label: 'Email',
                  nestedFields: [
                    new SelectFormFieldSchema<any>({
                      key: 'emailId',
                      label: 'Notification Email',
                      renderOptions: () =>
                        this.facades.emails.data().map((e) => ({
                          label: `${e.smtpServer} (${e.fromAddress})`,
                          value: e.id,
                        })),
                      colClass: 'col-6',
                    }),
                    new SelectFormFieldSchema<any>({
                      key: 'emailTemplateId',
                      label: 'Email Template',
                      renderOptions: () =>
                        this.facades.emailTemplates
                          .data()
                          .map((et) => ({ label: et.name, value: et.id })),
                      colClass: 'col-6',
                      onChange: (control) => {
                        const templateId = control?.value as string;
                        const selectedTemplate =
                          this.facades.emailTemplates
                            .data()
                            .find((et) => et.id === templateId) ?? null;
                        this._selectedEmailTemplate = selectedTemplate;
                      },
                    }),
                  ],
                }),
              ],
            }),
          ],
        }),
      );
    });
  }
}
