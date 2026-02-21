import { DataTableService } from '@obsluga-it/shared/data-table';
import { FormEntryVM } from './form.vm';

export class FormsDataTableService extends DataTableService<FormEntryVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        { field: 'id', label: 'ID', renderLink: (record) => ['..', record.id] },
        { field: 'formDefinitionId', label: 'Form Definition' },
        { field: 'createdAt', label: 'Created At', type: 'date' },
      ],
      persistenceKey: 'forms',
    });
  }
}
