import { DataTableService } from '@obsluga-it/shared/data-table';
import { EmailTemplateVM } from './email-template.vm';

export class SettingsEmailTemplateDataTableService extends DataTableService<EmailTemplateVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'name', label: 'Name', renderLink: (record) => ['..', record.id] },
        { field: 'subject', label: 'Subject' },
      ],
      persistenceKey: 'settings-email-templates',
    });
  }
}
