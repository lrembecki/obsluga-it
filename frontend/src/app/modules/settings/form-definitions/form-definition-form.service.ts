import { computed, effect, inject, signal } from '@angular/core';
import { Validators } from '@angular/forms';
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

  private readonly signals = {
    selectedEmailTemlpate: signal<EmailTemplateVM | null>(null),
    fields: signal<FormFieldDefinitionVM[]>([]),
  };

  private readonly options = {
    fields: computed(() =>
      this.signals.fields().map((f) => ({
        label: f.fieldName,
        value: f.fieldName,
      })),
    ),
    emails: computed(() =>
      this.facades.emails.data().map((e) => ({
        label: `${e.smtpServer} (${e.fromAddress})`,
        value: e.id,
      })),
    ),
    emailTemplates: computed(() =>
      this.facades.emailTemplates
        .data()
        .map((et) => ({ label: et.name, value: et.id })),
    ),
    emailTemplateFields: computed(() =>
      (this.signals.selectedEmailTemlpate()?.fields || [])
        .map((f) => ({ label: f, value: f }))
        .concat([
          { label: '@MAIL_TO', value: '@MAIL_TO' },
          { label: '@MAIL_BCC', value: '@MAIL_BCC' },
          { label: '@MAIL_SUBJECT', value: '@MAIL_SUBJECT' },
          { label: '@MAIL_CC', value: '@MAIL_CC' },
        ]),
    ),
  };

  private onChange(model: FormDefinitionVM) {
    if (model) {
      this.signals.fields.set(model.fields || []);
      this.signals.selectedEmailTemlpate.set(null);

      if (model.notification?.email?.emailTemplateId) {
        const selectedTemplate =
          this.facades.emailTemplates
            .data()
            .find(
              (et) => et.id === model.notification?.email?.emailTemplateId,
            ) ?? null;

        this.signals.selectedEmailTemlpate.set(selectedTemplate);
      }
    }
  }

  constructor() {
    super();

    this._returnRoute.set(['/modules/settings/form-definitions/list']);

    effect(() => {
      const model = this.model();
      this.onChange(model);

      this._schema.set(
        new FormSchema<FormDefinitionVM>({
          onChange: (formGroup) => {
            const formDefinition = formGroup.getRawValue() as FormDefinitionVM;
            console.log({ formDefinition });
            this.onChange(formDefinition);
          },
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
              onChange: (control) =>
                this.onChange(
                  control?.parent?.getRawValue() as FormDefinitionVM,
                ),
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
                  renderOptions: this.options.emailTemplateFields,
                  validators: [Validators.required],
                  colClass: 'col-6',
                }),
                new SelectFormFieldSchema<any>({
                  key: 'formDefinitionFieldName',
                  label: 'Form Definition Field Name',
                  renderOptions: this.options.fields,
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
                      renderOptions: this.options.emails,
                      colClass: 'col-6',
                    }),
                    new SelectFormFieldSchema<any>({
                      key: 'emailTemplateId',
                      label: 'Email Template',
                      renderOptions: this.options.emailTemplates,
                      colClass: 'col-6',
                      onChange: (control) =>
                        this.onChange(
                          control.parent?.parent?.getRawValue() as FormDefinitionVM,
                        ),
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
