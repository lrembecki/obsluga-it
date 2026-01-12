import { effect, inject } from '@angular/core';
import { ArrayFormService } from '../forms/form.service';
import { DataTableService } from './data-table.service';

export class DataTableFormService extends ArrayFormService<
  Record<string, any>
> {
  private readonly _dataTableService = inject(DataTableService);

  constructor() {
    super();

    effect(() => {
      if (this._dataTableService.schema()?.filter) {
        const filter = this._dataTableService.schema().filter!;
        this._schema.set(filter);
      }
    });
  }
}
