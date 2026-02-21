import { DataTableService } from '@obsluga-it/shared/data-table';
import { ContactVM } from './contact.vm';

export class SettingsContactDataTableService extends DataTableService<ContactVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        { field: 'name', label: 'Name', renderLink: (record) => ['..', record.id] },
        { field: 'email', label: 'Email' },
        { field: 'phone', label: 'Phone' },
      ],
      persistenceKey: 'settings-contacts',
    });
  }
}
