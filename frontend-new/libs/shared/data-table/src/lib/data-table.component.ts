import { NgClass } from '@angular/common';
import {
  Component,
  computed,
  effect,
  input,
  model,
  output,
  signal,
  Signal,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { DataTableColumnSchema, TableSort } from './data-table.types';

@Component({
  selector: 'app-data-table',
  standalone: true,
  imports: [NgClass, FormsModule, RouterLink],
  host: { role: 'grid', class: 'data-table' },
  styles: [`
    :host {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }
    table {
      width: 100%;
      border-collapse: collapse;
      border: 1px solid var(--border);
      border-radius: 4px;
      overflow: hidden;
    }
    thead {
      background: var(--bg-head);
    }
    thead th {
      text-align: left;
      font-weight: 600;
      padding: 0.75rem 1rem;
      border-bottom: 2px solid var(--border);
      border-right: 1px solid var(--border);
    }
    thead th:last-child {
      border-right: none;
    }
    tbody tr {
      transition: background-color 0.15s ease;
    }
    tbody tr:nth-child(odd) {
      background: var(--bg);
    }
    tbody tr:nth-child(even) {
      background: var(--bg-alt);
    }
    tbody tr:hover {
      background: var(--hover) !important;
      cursor: pointer;
    }
    tbody td {
      padding: 0.75rem 1rem;
      border-bottom: 1px solid var(--border);
      border-right: 1px solid var(--border);
    }
    tbody td:last-child {
      border-right: none;
    }
    tbody tr:last-child td {
      border-bottom: none;
    }
    .sortable {
      cursor: pointer;
      user-select: none;
    }
    .sortable button {
      display: flex;
      align-items: center;
      gap: 0.5rem;
    }
    .sort-indicator {
      font-size: 0.875rem;
      opacity: 0.7;
    }
    button.unstyled {
      background: none;
      border: none;
      padding: 0;
      font: inherit;
      cursor: pointer;
      color: inherit;
    }
  `],
  template: `
    <table>
      <thead>
        <tr>
          @for (col of orderedColumns(); track col.label + col.field) {
            <th [ngClass]="{ sortable: col.sortable }">
              <button
                type="button"
                class="unstyled"
                (click)="toggleSort(col)"
              >
                {{ col.label }}
                @if (col.sortable && getSortIndicator(col)) {
                  <span class="sort-indicator">{{ getSortIndicator(col) }}</span>
                }
              </button>
            </th>
          }
        </tr>
      </thead>
      <tbody>
        @for (row of visibleData(); track row[rowIdField()]) {
          <tr>
            @for (col of orderedColumns(); track col.label) {
              <td>
                @if (col.renderLink) {
                  <a [routerLink]="getRenderLink(col, row)">{{ renderCell(row, col) }}</a>
                } @else {
                  {{ renderCell(row, col) }}
                }
              </td>
            }
          </tr>
        }
      </tbody>
    </table>
  `,
})
export class DataTable<T extends { id: string }> {
  data = input.required<T[]>();
  columns = input.required<DataTableColumnSchema<T>[]>();
  rowIdField = input<keyof T & string>('id');
  persistenceKey = input<string | null | undefined>('default');

  orderBy = output<TableSort>();
  searchQuery = output<string>();

  protected readonly query = model<string>('');
  protected readonly sort = signal<TableSort | null>(null);
  protected readonly orderedColumns = signal<DataTableColumnSchema<T>[]>([]);

  constructor() {
    effect(() => this.orderedColumns.set(this.columns()));
  }

  protected readonly visibleData = computed<T[]>(() => {
    const q = this.query();
    const sort = this.sort();
    let rows = [...(this.data() ? this.data() : [])];

    if (q) {
      const cols = this.orderedColumns();
      rows = rows.filter((r) =>
        cols.some((c) =>
          String(r[c.field] ?? '').toLowerCase().includes(q.toLowerCase()),
        ),
      );
      this.searchQuery.emit(q);
    }

    if (sort) {
      const { column, direction } = sort;
      rows.sort((a: Record<string, unknown>, b: Record<string, unknown>) => {
        const as = String(a[column] ?? '');
        const bs = String(b[column] ?? '');
        return direction === 'asc' ? as.localeCompare(bs) : bs.localeCompare(as);
      });
    }

    return rows;
  });

  protected getSortIndicator(col: DataTableColumnSchema<T>): string {
    const s = this.sort();
    if (!s || s.column !== col.field) return '';
    return s.direction === 'asc' ? '↑' : '↓';
  }

  protected toggleSort(col: DataTableColumnSchema<T>): void {
    if (!col.sortable) return;
    const s = this.sort();
    if (!s || s.column !== col.field) {
      const next = { column: col.field, direction: 'asc' as const };
      this.sort.set(next);
      this.orderBy.emit(next);
      return;
    }
    if (s.direction === 'asc') {
      const next = { column: col.field, direction: 'desc' as const };
      this.sort.set(next);
      this.orderBy.emit(next);
    } else {
      this.sort.set(null);
    }
  }

  protected renderCell(row: T, col: DataTableColumnSchema<T>): string {
    if (col.render) {
      return col.render(row);
    }
    return String(this.getRowValue(row, col.field) ?? '');
  }

  protected getRenderLink(col: DataTableColumnSchema<T>, row: T): string[] {
    if (!col.renderLink) return [];
    const result = col.renderLink(row);
    return Array.isArray(result) ? result : [result.url];
  }

  protected getRowValue(row: T, field: string): any {
    return field.split('.').reduce((obj, key) => {
      return obj && (obj as any)[key] !== undefined ? (obj as any)[key] : null;
    }, row);
  }
}
