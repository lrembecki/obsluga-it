import { DataTableService } from '@obsluga-it/shared/data-table';
import { HowItWorksVM } from './how-it-works.vm';

export class TrotamundosHowItWorksDataTableService extends DataTableService<HowItWorksVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'title', label: 'Title', renderLink: (record) => ['..', record.id] },
      ],
      persistenceKey: 'trotamundos-how-it-works',
    });
  }
}
