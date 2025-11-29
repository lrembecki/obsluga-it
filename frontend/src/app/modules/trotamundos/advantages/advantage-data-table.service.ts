import { DataTableService } from '@app/shared/data-table/data-table.service';
import { AdvantageVM } from './advantage.vm';

export class TrotamundosAdvantageDataTableService extends DataTableService<AdvantageVM> {
  constructor() {
    super();

    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          field: 'title',
          label: 'Title',
          type: 'text',
          width: '200px',
          sortable: true,
          renderLink: (record: AdvantageVM) => ['..', record.id],
        },
        { field: 'content', label: 'Content', type: 'text', sortable: true },
      ],
      persistenceKey: 'trotamundos-advantages',
    });
  }
}
