import { DataTableService } from '@app/shared/data-table/data-table.service';
import { HighlightVM } from './highlight.vm';

export class TrotamundosHighlightDataTableService extends DataTableService<HighlightVM> {
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
          renderLink: (record: HighlightVM) => ['..', record.id],
        },
      ],
      persistenceKey: 'trotamundos-highlights',
    });
  }
}
