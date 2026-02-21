import { DataTableService } from '@obsluga-it/shared/data-table';
import { CompanyVM } from './company.vm';

export class SettingsCompanyDataTableService extends DataTableService<CompanyVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        { field: 'name', label: 'Name', renderLink: (record) => ['..', record.id] },
        { field: 'address' as any, label: 'Address', render: (row) => row.address?.address1 ?? '' },
        { field: 'address' as any, label: 'City', render: (row) => row.address?.city ?? '' },
        { field: 'address' as any, label: 'Country', render: (row) => row.address?.country ?? '' },
      ],
      persistenceKey: 'settings-companies',
    });
  }
}
