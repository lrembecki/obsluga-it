import { DataTableService } from '@app/shared/data-table/data-table.service';
import { IndividualTripVM } from './individual-trip.vm';

export class TrotamundosIndividualTripDataTableService extends DataTableService<IndividualTripVM> {
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
          renderLink: (record: IndividualTripVM) => ['..', record.id],
          render: (row: IndividualTripVM) => row.title || '(no title)',
        },
        {
          label: 'Description',
          field: 'description',
          render: (row: IndividualTripVM) => row.description || '',
        },
        {
          label: 'Items',
          field: 'items',
          width: '120px',
          render: (row: IndividualTripVM) => `${row.items?.length ?? 0}`,
        },
        {
          label: 'Steps',
          field: 'steps',
          width: '120px',
          render: (row: IndividualTripVM) => `${row.steps?.length ?? 0}`,
        },
      ],
      persistenceKey: 'trotamundos-individual-trips',
    });
  }
}
