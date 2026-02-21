import { DataTableService } from '@obsluga-it/shared/data-table';
import { FileVM } from './file.vm';

export class TrotamundosFileDataTableService extends DataTableService<FileVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'name', label: 'Name', renderLink: (record) => ['..', record.id] },
        { field: 'description', label: 'Description' },
      ],
      persistenceKey: 'trotamundos-files',
    });
  }
}
