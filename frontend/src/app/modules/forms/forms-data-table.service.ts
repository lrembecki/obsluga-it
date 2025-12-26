import { computed, effect, inject, signal } from '@angular/core';
import { DataTableService } from '@app/shared/data-table/data-table.service';
import { DataTableSchema } from '@app/shared/data-table/data-table.types';
import { SettingsFormDefinitionFacade } from '@modules/settings/form-definitions/form-definition.facade';

export class FormsDataTableService extends DataTableService<{
  id: string;
  name: string;
}> {
  private readonly _definitions = inject(SettingsFormDefinitionFacade);
  public readonly formDefinitionId = signal<string>(null!);
  private readonly selectedDefinition = computed(() => {
    const id = this.formDefinitionId();
    return this._definitions.data().find((def) => def.id === id);
  });

  private readonly schemaComputed = computed<DataTableSchema<any>>(() => ({
    quicksearch: true,
    sortable: true,
    columns: [
      {
        field: 'dateTime',
        label: 'Date & Time',
        type: 'date-time',
        renderLink: (record: { id: string }) => [
          `/modules/forms/${this.selectedDefinition()!.id}/${record.id}`,
        ],
      },
      ...(this.selectedDefinition()?.fields.map((item) => ({
        field: `fields.${item.fieldName}`,
        label: item.fieldName,
      })) || []),
    ],
  }));

  constructor() {
    super();

    this.canCreate.set(false);

    effect(() => {
      this._schema.set(this.schemaComputed());
    });
  }
}
