import { effect, inject } from '@angular/core';
import { DataTableService } from '@app/shared/data-table/data-table.service';
import { TrotamundosAdvantageFacade } from './advantage.facade';
import { AdvantageVM } from './advantage.vm';

export class TrotamundosAdvantageDataTableService extends DataTableService<AdvantageVM> {
  private readonly _facade = inject(TrotamundosAdvantageFacade);

  constructor() {
    super();

    effect(() => this._data.set(this._facade.data()));

    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          field: 'title',
          label: 'Title',
          type: 'text',
          width: '200px',
          sortable: true,
          renderLink: (record: AdvantageVM) => ['..', record.id],
        },
        { field: 'content', label: 'Content', type: 'text', sortable: true },
      ],
      persistenceKey: 'trotamundos-advantages',
    });
  }

  override initialize(): Promise<void> {
    return this._facade.initialize();
  }
}
