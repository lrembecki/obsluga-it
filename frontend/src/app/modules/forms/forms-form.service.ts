import { effect, inject } from '@angular/core';
import {
  DateFormFieldSchema,
  FormFieldSchema,
  FormSchema,
  GroupFormFieldSchema,
  TextareaFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import { FormFieldDefinitionVM } from '../settings/form-definitions/form-definition.vm';
import { FormSessionService } from './forms.session';

export class FormsFormService extends FormService<{
  id: string;
  name: string;
}> {
  private readonly _session = inject(FormSessionService);

  constructor() {
    super();

    effect(() => {
      const formDefinitionId = this._session.formDefinitionId();
      const formDefinition = this._session.formDefinition();

      this._returnRoute.set(['/modules/forms/', formDefinitionId!]);

      if (!formDefinition) {
        return;
      }

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
              nestedFields: formDefinition.fields.map(
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
