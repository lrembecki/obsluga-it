import { DataTableService } from '@app/shared/data-table/data-table.service';

export class FormsDataTableService extends DataTableService<{
  id: string;
  name: string;
}> {
  constructor() {
    super();

    this.canCreate.set(false);

    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          field: 'name',
          label: 'Name',
          renderLink: (record) => ['..', record.id],
        },
      ],
      persistenceKey: 'forms',
    });
  }
}
