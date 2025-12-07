import { DataTableService } from '@app/shared/data-table/data-table.service';
import { EmailVM } from './email.vm';

export class SettingsEmailDataTableService extends DataTableService<EmailVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          field: 'smtpServer',
          label: 'Server',
          renderLink: (record) => ['..', record.id],
        },
        { field: 'isActive', label: 'Is Active' },
        { field: 'smtpUsername', label: 'Username' },
        { field: 'fromAddress', label: 'From Address' },
      ],
      persistenceKey: 'settings-emails',
    });
  }
}
