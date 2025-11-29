import { Component, inject } from '@angular/core';
import { Button } from '../ui/button/button.js';
import { UiPanel } from '../ui/ui-panel.js';
import { DataTable } from './data-table.js';
import { DataTableService } from './data-table.service.js';

@Component({
  imports: [UiPanel, Button, DataTable],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-data-table
      [data]="_service.data()"
      [columns]="_service.columns()"
      [persistenceKey]="_service.schema().persistenceKey"
      (orderBy)="onOrderBy($event)"
      (searchQuery)="onSearch($event)"
    />
  `,
  styles: ``,
})
export class DataTableTemplate {
  protected readonly _service = inject(DataTableService);

  async ngOnInit() {
    await this._service.facade.initialize();
  }

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._service.facade.filter({ sort });
  }

  protected onSearch(query: string) {
    this._service.facade.filter({ query });
  }
}
