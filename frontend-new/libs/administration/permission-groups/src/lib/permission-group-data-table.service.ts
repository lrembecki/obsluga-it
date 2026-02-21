import { DataTableService } from '@obsluga-it/shared/data-table';
import { PermissionGroupVM } from './permission-group.vm';

export class AdministrationPermissionGroupDataTableService extends DataTableService<PermissionGroupVM> {
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
      persistenceKey: 'administration-permission-groups',
    });
  }
}
