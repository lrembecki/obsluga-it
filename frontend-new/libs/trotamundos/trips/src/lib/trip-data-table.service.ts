import { DataTableService } from '@obsluga-it/shared/data-table';
import { TripVM } from './trip.vm';

export class TrotamundosTripDataTableService extends DataTableService<TripVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'name', label: 'Name', renderLink: (record) => ['..', record.id] },
        { field: 'startDate', label: 'Start Date', type: 'date' },
        { field: 'endDate', label: 'End Date', type: 'date' },
      ],
      persistenceKey: 'trotamundos-trips',
    });
  }
}
