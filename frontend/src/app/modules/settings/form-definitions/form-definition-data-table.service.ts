import { DataTableService } from '@app/shared/data-table/data-table.service';
import { FormDefinitionVM } from './form-definition.vm';

export class SettingsFormDefinitionDataTableService extends DataTableService<FormDefinitionVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          field: 'name',
          label: 'Name',
          renderLink: (record) => ['..', record.id],
        },
        {
          field: 'fields',
          label: 'Fields',
          type: 'number',
          width: '120px',
          render: (row) => `${row.fields?.length ?? 0}`,
        },
      ],
      persistenceKey: 'settings-form-definitions',
    });
  }
}
