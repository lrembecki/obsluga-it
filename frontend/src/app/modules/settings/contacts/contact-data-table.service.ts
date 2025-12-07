import { DataTableService } from '@app/shared/data-table/data-table.service';
import { ContactModel } from './contact.model';

export class SettingsContactDataTableService extends DataTableService<ContactModel> {
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
        { field: 'email', label: 'Email', width: '200px' },
        { field: 'phone', label: 'Phone', width: '200px' },
        { field: 'position', label: 'Position', width: '250px' },
        { field: 'isActive', label: 'Is Active', width: '120px' },
      ],
      persistenceKey: 'settings-contacts',
    });
  }
}
