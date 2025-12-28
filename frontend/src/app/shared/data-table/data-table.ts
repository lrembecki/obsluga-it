import { NgClass, NgTemplateOutlet } from '@angular/common';
import {
  Component,
  computed,
  input,
  model,
  output,
  signal,
  Signal,
} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@app/core/pipes/date-pipe';
import { Checkbox } from 'primeng/checkbox';
import { Button } from '../ui/button/button';
import { TextInputComponent } from '../ui/inputs/text-input.component';
import { DataTableRenderLink } from './data-table-render-link';
import { DataTableColumnSchema, TableSort } from './data-table.types';

@Component({
  selector: 'app-data-table',
  standalone: true,
  imports: [
    NgClass,
    Button,
    TextInputComponent,
    FormsModule,
    ReactiveFormsModule,
    Checkbox,
    DatePipe,
    NgTemplateOutlet,
    DataTableRenderLink,
  ],
  host: { role: 'grid', class: 'data-table' },
  styles: [
    `
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

      .sortable button:hover {
        color: var(--ring);
      }

      .sort-indicator {
        font-size: 0.875rem;
        opacity: 0.7;
      }

      .sortable button:hover .sort-indicator {
        opacity: 1;
      }

      .drag-handle {
        cursor: move;
        opacity: 0.5;
        margin-right: 0.5rem;
        font-size: 1.1rem;
      }

      .drag-handle:hover {
        opacity: 1;
      }

      .actions {
        display: flex;
        gap: 0.5rem;
        justify-content: flex-end;
      }

      button.unstyled {
        background: none;
        border: none;
        padding: 0;
        font: inherit;
        cursor: pointer;
        color: inherit;
      }
    `,
  ],
  template: `
    <table>
      <thead>
        <tr>
          @for (col of orderedColumns(); track col.label) {
            <th
              [ngClass]="{ sortable: col.sortable }"
              draggable="{{ isReorderable() ? 'true' : 'false' }}"
              (dragstart)="onHeaderDragStart($index)"
              (dragover)="onHeaderDragOver($event)"
              (drop)="onHeaderDrop($index)"
            >
              @if (isReorderable()) {
                <span class="drag-handle">☰</span>
              }
              <button
                type="button"
                class="unstyled"
                (click)="toggleSort(col)"
                [attr.aria-sort]="ariaSort(col)"
              >
                {{ col.label }}
                @if (col.sortable && getSortIndicator(col)) {
                  <span class="sort-indicator">{{
                    getSortIndicator(col)
                  }}</span>
                }
              </button>
            </th>
          }
          @if (isEditable()) {
            <th class="actions">{{ actionsLabel() }}</th>
          }
        </tr>
      </thead>
      <tbody>
        <ng-template #renderCellTemplate let-row="row" let-col="col">
          @if (col.type === 'boolean') {
            <p-checkbox
              binary="true"
              [disabled]="true"
              [(ngModel)]="row[col.field]"
              [ngModelOptions]="{ standalone: true }"
            />
          } @else if (col.type === 'date') {
            @let value = getRowValue(row, col.field);
            {{ value | date: 'shortDate' }}
          } @else if (col.type === 'date-time') {
            @let value = getRowValue(row, col.field);
            {{ value | date: 'short' }}
          } @else {
            {{ renderCell(row, col) }}
          }
        </ng-template>

        @for (row of visibleData(); track row[rowIdField()]) {
          <tr>
            @for (col of orderedColumns(); track col.label) {
              <td>
                @if (isEditing(row)) {
                  @switch (col.type) {
                    @case ('text') {
                      <app-text-input
                        [label]="col.label"
                        [value]="getEditValue(col)"
                        (valueChange)="setEditValue(col, $event)"
                      />
                    }
                    @default {
                      <app-text-input
                        [label]="col.label"
                        [value]="getEditValue(col)"
                        (valueChange)="setEditValue(col, $event)"
                      />
                    }
                  }
                } @else {
                  @if (col.renderLink) {
                    <app-data-table-render-link [column]="col" [row]="row">
                      <ng-container
                        *ngTemplateOutlet="
                          renderCellTemplate;
                          context: { row: row, col: col }
                        "
                      />
                    </app-data-table-render-link>
                  } @else {
                    <ng-container
                      *ngTemplateOutlet="
                        renderCellTemplate;
                        context: { row: row, col: col }
                      "
                    />
                  }
                }
              </td>
            }
            @if (isEditing(row) || isEditable()) {
              <td class="actions">
                @if (isEditing(row)) {
                  <app-button
                    [text]="saveLabel()"
                    color="primary"
                    (buttonClick)="saveRow()"
                  />
                  <app-button
                    [text]="cancelLabel()"
                    (buttonClick)="cancelEdit()"
                  />
                } @else {
                  @if (isEditable()) {
                    <app-button
                      [text]="editLabel()"
                      (buttonClick)="startEdit(row)"
                    />
                  }
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
  // Inputs
  data = input.required<T[]>();
  columns = input.required<DataTableColumnSchema<T>[]>();
  rowIdField = input<keyof T & string>('id');
  loading = input<boolean | Signal<boolean>>();
  saving = input<boolean | Signal<boolean>>();
  deleting = input<boolean | Signal<boolean>>();
  searchPlaceholder = input<string>('dataTable.search.placeholder');
  actionsLabel = input<string>('DATA_TABLE.ACTIONS');
  editLabel = input<string>('DATA_TABLE.EDIT');
  saveLabel = input<string>('DATA_TABLE.SAVE');
  cancelLabel = input<string>('DATA_TABLE.CANCEL');
  persistenceKey = input<string | null | undefined>('default');
  isEditable = input<boolean>(false);
  isReorderable = input<boolean>(false);
  isQuicksearch = input<boolean>(false);

  // Outputs
  save = output<T>();
  orderBy = output<TableSort>();
  columnsChange = output<DataTableColumnSchema<T>[]>();
  searchQuery = output<string>();

  // Local signals
  protected readonly query = model<string>('');
  protected readonly sort = signal<TableSort | null>(null);
  protected readonly editingId = signal<string | null>(null);
  protected readonly editDraft = signal<Partial<T> | null>(null);
  protected readonly orderedColumns = signal<DataTableColumnSchema<T>[]>([]);
  protected dragSourceIndex = -1;

  ngOnInit(): void {
    this.orderedColumns.set(this.columns());
    // columns are taken from input; DI handled in template variant
  }

  protected readonly visibleData = computed<T[]>(() => {
    const q = this.query();
    const sort = this.sort();
    let rows = [...(this.data() ? this.data() : [])];

    if (q) {
      const cols = this.orderedColumns();
      rows = rows.filter((r) =>
        cols.some((c) =>
          String(r[c.field] ?? '')
            .toLowerCase()
            .includes(q.toLowerCase()),
        ),
      );
      this.searchQuery.emit(q);
    }

    if (sort) {
      const { column, direction } = sort;
      rows.sort((a: Record<string, unknown>, b: Record<string, unknown>) => {
        const av = a[column];
        const bv = b[column];
        const as = String(av ?? '');
        const bs = String(bv ?? '');
        return direction === 'asc'
          ? as.localeCompare(bs)
          : bs.localeCompare(as);
      });
    }

    return rows;
  });

  protected ariaSort(
    col: DataTableColumnSchema<T>,
  ): 'none' | 'ascending' | 'descending' {
    const s = this.sort();
    if (!s || s.column !== col.field) return 'none';
    return s.direction === 'asc' ? 'ascending' : 'descending';
  }

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
      // consumer can treat absence of sort as reset
    }
  }

  // Drag & Drop headers for column reordering
  protected onHeaderDragStart(index: number): void {
    this.dragSourceIndex = index;
  }

  protected onHeaderDragOver(event: DragEvent): void {
    event.preventDefault();
  }

  protected onHeaderDrop(targetIndex: number): void {
    if (this.dragSourceIndex < 0 || targetIndex === this.dragSourceIndex)
      return;
    const cols = [...this.orderedColumns()];
    const [moved] = cols.splice(this.dragSourceIndex, 1);
    cols.splice(targetIndex, 0, moved);
    this.orderedColumns.set(cols);
    this.columnsChange.emit(cols);
    this.persistColumnOrder(cols);
    this.dragSourceIndex = -1;
  }

  protected persistColumnOrder(cols: DataTableColumnSchema<T>[]): void {
    try {
      const key = `data-table:columns:${this.persistenceKey()}:${this.rowIdField()}`;
      const value = JSON.stringify(
        cols.map((c) => ({ field: c.field, label: c.label })),
      );
      localStorage.setItem(key, value);
    } catch {
      // persistence optional; ignore errors
    }
  }

  ngAfterViewInit(): void {
    try {
      const key = `data-table:columns:${this.persistenceKey()}:${this.rowIdField()}`;
      const raw = localStorage.getItem(key);
      if (!raw) return;
      const order: Array<{ field: string; label: string }> = JSON.parse(raw);
      const map = new Map(order.map((o) => [o.field, o]));
      const current = [...this.columns()];
      current.sort((a, b) => {
        const ai = map.has(a.field)
          ? order.findIndex((o) => o.field === a.field)
          : Number.MAX_SAFE_INTEGER;
        const bi = map.has(b.field)
          ? order.findIndex((o) => o.field === b.field)
          : Number.MAX_SAFE_INTEGER;
        return ai - bi;
      });
      this.orderedColumns.set(current);
    } catch {
      // restore optional; ignore errors
    }
  }

  protected startEdit(row: T): void {
    const idField = this.rowIdField();
    this.editingId.set(String(row[idField]));
    this.editDraft.set({ ...row });
  }

  protected isEditing(row: T): boolean {
    const idField = this.rowIdField();
    return this.editingId() === String(row[idField]);
  }

  protected renderCell(row: T, col: DataTableColumnSchema<T>): string {
    if (col.render) {
      return col.render(row);
    }

    return String(this.getRowValue(row, col.field) ?? '');
  }

  protected saveRow(): void {
    const draft = this.editDraft();
    if (!draft) return;
    this.save.emit(draft as T);
    this.cancelEdit();
  }

  protected cancelEdit(): void {
    this.editingId.set(null);
    this.editDraft.set(null);
  }

  protected getEditValue(col: DataTableColumnSchema<T>): string | undefined {
    const draft = this.editDraft();
    if (!draft) return undefined;
    return String((draft as any)[col.field] ?? '');
  }

  protected setEditValue(
    col: DataTableColumnSchema<T>,
    value: string | undefined,
  ): void {
    const draft = this.editDraft();
    if (!draft) return;
    (draft as any)[col.field] = value;
    this.editDraft.set({ ...(draft as any) });
  }

  protected getRowValue(row: T, field: string): any {
    const value = field.split('.').reduce((obj, key) => {
      return obj && (obj as any)[key] !== undefined ? (obj as any)[key] : null;
    }, row);
    return value;
  }
}
