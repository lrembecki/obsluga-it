import { DataTableService } from '@obsluga-it/shared/data-table';
import { WebsiteVM } from './website.vm';

export class SettingsWebsiteDataTableService extends DataTableService<WebsiteVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'name', label: 'Name', renderLink: (record) => ['..', record.id] },
        { field: 'url', label: 'URL' },
      ],
      persistenceKey: 'settings-websites',
    });
  }
}
