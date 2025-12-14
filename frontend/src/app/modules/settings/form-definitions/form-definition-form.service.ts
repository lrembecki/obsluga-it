import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  CheckboxFormFieldSchema,
  CollectionFormFieldSchema,
  FormSchema,
  SelectFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import {
  FormDefinitionFieldType,
  FormDefinitionVM,
  FormFieldDefinitionVM,
} from './form-definition.vm';

const fieldTypeOptions: Array<{ label: string; value: FormDefinitionFieldType }> =
  [
    { label: 'Text', value: 'text' },
    { label: 'Email', value: 'email' },
    { label: 'Number', value: 'number' },
    { label: 'Date', value: 'date' },
    { label: 'Textarea', value: 'textarea' },
    { label: 'Select', value: 'select' },
    { label: 'Checkbox', value: 'checkbox' },
    { label: 'Radio', value: 'radio' },
  ];

export class SettingsFormDefinitionFormService extends FormService<FormDefinitionVM> {
  constructor() {
    super();

    this._returnRoute.set(['/modules/settings/form-definitions/list']);

    effect(() => {
      this._schema.set(
        new FormSchema<FormDefinitionVM>({
          fields: [
            new TextFormFieldSchema<FormDefinitionVM>({
              key: 'name',
              label: 'Name',
              validators: [Validators.required],
            }),
            new CollectionFormFieldSchema<FormDefinitionVM, FormFieldDefinitionVM>({
              key: 'fields',
              label: 'Fields',
              layout: 'vertical',
              itemColClass: 'col-3',
              emptyText: 'No fields',
              itemFields: [
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
                }),
              ],
            }),
          ],
        }),
      );
    });
  }
}
