import { DataTableService } from '@obsluga-it/shared/data-table';
import { FormDefinitionVM } from './form-definition.vm';

export class SettingsFormDefinitionDataTableService extends DataTableService<FormDefinitionVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'name', label: 'Name', renderLink: (record) => ['..', record.id] },
        { field: 'description', label: 'Description' },
      ],
      persistenceKey: 'settings-form-definitions',
    });
  }
}
