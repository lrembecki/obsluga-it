import { DataTableService } from '@obsluga-it/shared/data-table';
import { AboutUsVM } from './about-us.vm';

export class TrotamundosAboutUsDataTableService extends DataTableService<AboutUsVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true, sortable: true,
      columns: [
        { field: 'title', label: 'Title', renderLink: (record) => ['..', record.id] },
      ],
      persistenceKey: 'trotamundos-about-us',
    });
  }
}
