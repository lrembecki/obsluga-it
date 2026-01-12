import { DataTableService } from '@app/shared/data-table/data-table.service';
import { AboutUsVM } from './about-us.vm';

export class TrotamundosAboutUsDataTableService extends DataTableService<AboutUsVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          label: 'Title',
          field: 'title',
          width: '240px',
          renderLink: (record: AboutUsVM) => ['..', record.id],
          render: (row: AboutUsVM) => row.title || '(no title)',
        },
        {
          label: 'Description',
          field: 'description',
          render: (row: AboutUsVM) => row.description || '',
        },
        {
          label: 'Items',
          field: 'items',
          width: '120px',
          render: (row: AboutUsVM) => `${row.items?.length ?? 0}`,
        },
      ],
      persistenceKey: 'trotamundos-about-us',
    });
  }
}
