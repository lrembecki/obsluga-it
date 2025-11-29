import { DataTableService } from '@app/shared/data-table/data-table.service';
import { LoyalityProgramVM } from './loyality-program.vm';

export class TrotamundosLoyalityProgramDataTableService extends DataTableService<LoyalityProgramVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          field: 'name',
          label: 'Name',
          type: 'text',
          width: '300px',
          sortable: true,
          renderLink: (record) => ['..', record.id],
        },
        {
          field: 'title',
          label: 'Title',
          type: 'text',
          width: '300px',
          sortable: true,
        },
        {
          field: 'description',
          label: 'Description',
          type: 'text',
          sortable: true,
        },
      ],
      persistenceKey: 'trotamundos-loyality-programs',
    });
  }
}
