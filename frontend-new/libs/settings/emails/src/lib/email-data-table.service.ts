import { DataTableService } from '@obsluga-it/shared/data-table';
import { EmailVM } from './email.vm';

export class SettingsEmailDataTableService extends DataTableService<EmailVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'name', label: 'Name', renderLink: (record) => ['..', record.id] },
        { field: 'address', label: 'Address' },
      ],
      persistenceKey: 'settings-emails',
    });
  }
}
