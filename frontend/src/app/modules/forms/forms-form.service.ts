import { computed, effect, inject, signal } from '@angular/core';
import {
  DateFormFieldSchema,
  FormFieldSchema,
  FormSchema,
  GroupFormFieldSchema,
  TextareaFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import { SettingsFormDefinitionFacade } from '@modules/settings/form-definitions/form-definition.facade';
import { FormFieldDefinitionVM } from '../settings/form-definitions/form-definition.vm';
import { FormsFacade } from './forms.facade';

export class FormsFormService extends FormService<{
  id: string;
  name: string;
}> {
  private readonly facades = {
    definitions: inject(SettingsFormDefinitionFacade),
    forms: inject(FormsFacade),
  };
  public readonly formDefinitionId = signal<string>(null!);
  private readonly selectedDefinition = computed(() => {
    const id = this.formDefinitionId();
    return this.facades.definitions.data().find((def) => def.id === id);
  });

  constructor() {
    super();

    effect(() => {
      this._returnRoute.set(['/modules/forms/', this.formDefinitionId()]);
      this._schema.set(
        new FormSchema<any>({
          fields: [
            new DateFormFieldSchema({
              key: 'dateTime',
              label: 'Date & Time',
              disabled: true,
              withTime: true,
            }),
            new GroupFormFieldSchema({
              key: 'fields',
              label: 'Form Fields',
              nestedFields: this.selectedDefinition()!.fields.map(
                this.createFieldSchema.bind(this),
              ),
            }),
          ],
        }),
      );
    });
  }

  private createFieldSchema(item: FormFieldDefinitionVM): FormFieldSchema<any> {
    switch (item.fieldType) {
      case 'email':
      case 'text':
        return new TextFormFieldSchema({
          key: item.fieldName,
          label: item.fieldName,
          disabled: true,
        });
      case 'date':
      case 'date-time':
        return new DateFormFieldSchema({
          key: item.fieldName,
          label: item.fieldName,
          withTime: item.fieldType === 'date-time',
          disabled: true,
        });
      case 'textarea':
        return new TextareaFormFieldSchema({
          key: item.fieldName,
          label: item.fieldName,
          disabled: true,
        });
      default:
        throw new Error(`Unsupported field type: ${item.fieldType}`);
    }
  }
}
