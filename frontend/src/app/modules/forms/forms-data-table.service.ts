import { effect, inject, linkedSignal } from '@angular/core';
import { DataTableService } from '@app/shared/data-table/data-table.service';

import { FormFilterVM } from './form-filter.vm';
import { FormSessionService } from './forms.session';

export class FormsDataTableService extends DataTableService<{
  id: string;
  name: string;
  [key: string]: any;
}> {
  private readonly _session = inject(FormSessionService);
  private readonly _filter = linkedSignal<FormFilterVM>(() => {
    const formDefinitionId = this._session.formDefinitionId();
    return new FormFilterVM({
      formDefinitionId: formDefinitionId!,
    });
  });

  constructor() {
    super();

    this.canCreate.set(false);

    const rawFilterData = sessionStorage.getItem(
      `forms-data-table-${this._session.formDefinitionId()}-filter`,
    );
    if (rawFilterData) {
      const filterData = JSON.parse(rawFilterData);
      this._filter.set(new FormFilterVM(filterData));
    }

    effect(() => {
      const formDefinitionId = this._session.formDefinitionId();
      const formDefinition = this._session.formDefinition();

      this._schema.set(null!);

      if (!formDefinition) {
        return;
      }

      this._schema.set({
        quicksearch: true,
        sortable: true,
        persistenceKey: `forms-data-table-${formDefinitionId}`,
        filter: this._filterSchema(),
        columns: [
          {
            field: 'createdAt',
            label: 'Date & Time',
            type: 'date-time',
            renderLink: (record: { id: string }) => [
              `/modules/forms/${formDefinitionId}/${record.id}`,
            ],
            sortable: true,
          },
          ...formDefinition.fields.map((item) => ({
            field: `fields.${item.fieldName}`,
            label: item.fieldName,
          })),
        ],
      });
    });
  }
}
