import { DataTableService } from '@app/shared/data-table/data-table.service';
import { HowItWorksVM } from './how-it-works.vm';

export class TrotamundosHowItWorksDataTableService extends DataTableService<HowItWorksVM> {
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
          renderLink: (record: HowItWorksVM) => ['..', record.id],
          render: (row: HowItWorksVM) => row.title || '(no title)',
        },
        {
          label: 'Header Text',
          field: 'headerText',
          render: (row: HowItWorksVM) => row.headerText || '',
        },
        {
          label: 'Items',
          field: 'items',
          width: '120px',
          render: (row: HowItWorksVM) => `${row.items?.length ?? 0}`,
        },
      ],
      persistenceKey: 'trotamundos-how-it-works',
    });
  }
}
