import { computed, effect, inject, signal } from '@angular/core';
import {
  DateFormFieldSchema,
  FormSchema,
  GroupFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import { SettingsFormDefinitionFacade } from '@modules/settings/form-definitions/form-definition.facade';
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
                (item) =>
                  new TextFormFieldSchema({
                    key: `${item.fieldName}`,
                    label: item.fieldName,
                    disabled: true,
                  }),
              ),
            }),
          ],
        }),
      );
    });
  }
}
