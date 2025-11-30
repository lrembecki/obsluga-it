import { DataTableService } from '@app/shared/data-table/data-table.service';
import { FileVM } from './file.vm';

export class TrotamundosFileDataTableService extends DataTableService<FileVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          field: 'displayName',
          label: 'Display Name',
          type: 'text',
          width: '300px',
          sortable: true,
          renderLink: (record) => ['..', record.id],
        },
        {
          field: 'position',
          label: 'Position',
          type: 'number',
          width: '80px',
          sortable: true,
        },
        {
          field: 'group',
          label: 'Group',
          type: 'text',
          width: '200px',
          sortable: true,
        },
        {
          field: 'description',
          label: 'Description',
          type: 'text',
          width: '300px',
          sortable: true,
        },
      ],
      persistenceKey: 'trotamundos-files',
    });
  }
}
