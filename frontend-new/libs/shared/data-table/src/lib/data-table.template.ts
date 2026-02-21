import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DataTable } from './data-table.component';
import { DataTableService } from './data-table.service';

@Component({
  standalone: true,
  imports: [DataTable, RouterLink],
  template: `
    @if (_service.facade.loading()) {
      <p>Loading data...</p>
    } @else {
      <div class="data-table-header">
        @if (_service.canCreate()) {
          <a routerLink="../create" class="btn-create">Create</a>
        }
      </div>

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
  styles: [`
    :host {
      display: block;
      position: relative;
      min-height: 200px;
    }
    .data-table-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 1rem;
    }
    .btn-create {
      padding: 0.5rem 1rem;
      background: var(--primary);
      color: white;
      border-radius: 4px;
      text-decoration: none;
      cursor: pointer;
    }
  `],
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
