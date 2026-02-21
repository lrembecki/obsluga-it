import { DataTableService } from '@obsluga-it/shared/data-table';
import { AdvantageVM } from './advantage.vm';

export class TrotamundosAdvantageDataTableService extends DataTableService<AdvantageVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'title', label: 'Title', renderLink: (record) => ['..', record.id] },
        { field: 'content', label: 'Content' },
      ],
      persistenceKey: 'trotamundos-advantages',
    });
  }
}
