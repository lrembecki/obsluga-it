import { DataTableService } from '@app/shared/data-table/data-table.service';
import { CompanyVM } from './company.vm';

export class SettingsCompanyDataTableService extends DataTableService<any> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          label: 'Name',
          field: 'name',
          width: '200px',
          renderLink: (record: CompanyVM) => ['..', record.id],
          render: (row: CompanyVM) => row.name || '(no name)',
        },
        {
          label: 'Address',
          field: 'address.address1',
          render: (row: CompanyVM) => row.address?.address1 || '',
        },
        {
          label: 'City',
          field: 'address.city',
          width: '150px',
          render: (row: CompanyVM) => row.address?.city || '',
        },
        {
          label: 'Country',
          field: 'address.country',
          width: '150px',
          render: (row: CompanyVM) => row.address?.country || '',
        },
        {
          label: 'Parameters',
          field: 'parameters',
          width: '120px',
          render: (row: CompanyVM) => `${row.parameters?.length ?? 0}`,
        },
      ],
      persistenceKey: 'settings-companies',
    });
  }
}
