import { DataTableService } from '@app/shared/data-table/data-table.service';
import { WebsiteVM } from './website.vm';

export class SettingsWebsiteDataTableService extends DataTableService<any> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          label: 'Title',
          field: 'title',
          width: '220px',
          renderLink: (record: WebsiteVM) => ['..', record.id],
          render: (row: WebsiteVM) => row.title || '(no title)',
        },
        {
          label: 'Meta Title',
          field: 'meta.title',
          width: '220px',
          render: (row: WebsiteVM) => row.meta?.title || '',
        },
      ],
      persistenceKey: 'settings-websites',
    });
  }
}
