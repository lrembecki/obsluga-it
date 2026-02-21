import { DataTableService } from '@obsluga-it/shared/data-table';
import { HighlightVM } from './highlight.vm';

export class TrotamundosHighlightDataTableService extends DataTableService<HighlightVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'title', label: 'Title', renderLink: (record) => ['..', record.id] },
        { field: 'icon', label: 'Icon' },
      ],
      persistenceKey: 'trotamundos-highlights',
    });
  }
}
