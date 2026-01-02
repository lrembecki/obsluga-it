import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Button } from '../ui/button/button.js';
import { LoadingComponent } from '../ui/loading/loading.component.js';
import { UiPanel } from '../ui/ui-panel.js';
import { DataTableFormRenderer } from './data-table-form-renderer';
import { DataTable } from './data-table.js';
import { DataTableService } from './data-table.service.js';

@Component({
  imports: [
    UiPanel,
    Button,
    DataTable,
    LoadingComponent,
    RouterLink,
    DataTableFormRenderer,
  ],
  template: `
    @if (_service.facade.loading()) {
      <app-loading [text]="'Loading data'" />
    } @else {
      <app-ui-panel>
        <ng-template #start>
          @if (_service.canCreate()) {
            <app-button color="primary" text="Create" routerLink="../create" />
          }
        </ng-template>

        @if (_service.schema() && _service.schema().filter) {
          <ng-template #end>
            <app-data-table-form-renderer />
          </ng-template>
        }
      </app-ui-panel>

      @if (_service.schema()) {
        <app-data-table
          [data]="_service.data()"
          [columns]="_service.columns()"
          [persistenceKey]="_service.persistenceKey()"
          (orderBy)="onOrderBy($event)"
          (searchQuery)="onSearch($event)"
        />
      }
    }
  `,
  styles: `
    :host {
      display: block;
      position: relative;
      min-height: 200px;
    }
  `,
})
export class DataTableTemplate {
  protected readonly _service = inject(DataTableService);
  private readonly _cdr = inject(ChangeDetectorRef);

  async ngOnInit() {
    await this._service.facade.populate();
    this._cdr.detectChanges();
  }

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._service.facade.sort(sort);
  }

  protected onSearch(query: string) {
    this._service.facade.filter({ query });
  }
}
