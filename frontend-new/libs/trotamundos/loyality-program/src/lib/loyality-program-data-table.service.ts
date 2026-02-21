import { DataTableService } from '@obsluga-it/shared/data-table';
import { LoyalityProgramVM } from './loyality-program.vm';

export class TrotamundosLoyalityProgramDataTableService extends DataTableService<LoyalityProgramVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'name', label: 'Name', renderLink: (record) => ['..', record.id] },
        { field: 'title', label: 'Title' },
      ],
      persistenceKey: 'trotamundos-loyality-program',
    });
  }
}
