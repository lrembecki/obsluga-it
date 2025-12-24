import { DataTableService } from '@app/shared/data-table/data-table.service';
import { TripVM } from './trip.vm';

export class TrotamundosTripDataTableService extends DataTableService<TripVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          label: 'Name',
          field: 'name',
          width: '180px',
          renderLink: (record: TripVM) => ['..', record.id],
          render: (row: TripVM) => row.name || '(no name)',
        },
        { label: 'Title', field: 'title' },
        { label: 'Subtitle', field: 'subtitle' },
        {
          label: 'Start Date',
          field: 'startDate',
          width: '140px',
          type: 'date',
        },
        {
          label: 'End Date',
          field: 'endDate',
          width: '140px',
          type: 'date',
        },
        { label: 'Calendar', field: 'calendar', width: '160px' },
        { label: 'Active', field: 'isActive', width: '100px', type: 'boolean' },
        {
          label: 'Disabled',
          field: 'isDisabled',
          width: '110px',
          type: 'boolean',
        },
      ],
      persistenceKey: 'trotamundos-trips',
    });
  }
}
