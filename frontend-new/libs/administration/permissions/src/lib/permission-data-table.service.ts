import { DataTableService } from '@obsluga-it/shared/data-table';
import { PermissionVM } from './permission.vm';

export class AdministrationPermissionDataTableService extends DataTableService<PermissionVM> {
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
      ],
      persistenceKey: 'administration-permissions',
    });
  }
}
