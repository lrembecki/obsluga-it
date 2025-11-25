import { NgComponentOutlet, NgTemplateOutlet } from '@angular/common';
import {
  Component,
  computed,
  contentChildren,
  input,
  signal,
} from '@angular/core';
import { TableModule } from 'primeng/table';
import { LoadingComponent } from './loading/loading.component';
import { UiTableColumn } from './ui-table-column';

@Component({
  selector: 'app-ui-table',
  imports: [TableModule, NgComponentOutlet, NgTemplateOutlet, LoadingComponent],
  template: `
    <div class="table-container">
      @if (loading()) {
        <app-loading />
      }

      <div class="table-wrapper">
        <table class="app-ui-table" role="grid" [class.loading]="loading()">
          <thead>
            <tr>
              @for (column of columns(); track column) {
                <th
                  scope="col"
                  [style.min-width]="column.minWidth()"
                  [style.width]="column.width()"
                >
                  {{ column.text() }}
                </th>
              }
            </tr>
          </thead>
          <tbody>
            @for (record of data(); track record) {
              <tr>
                @for (column of columns(); track column) {
                  <td>
                    @if (column.cellTemplate()) {
                      <ng-container
                        [ngTemplateOutlet]="column.cellTemplate()"
                        [ngTemplateOutletContext]="{
                          column,
                          record,
                          table: this,
                        }"
                      />
                    } @else {
                      <ng-container
                        [ngComponentOutlet]="column.template()"
                        [ngComponentOutletInputs]="{
                          column,
                          record,
                          table: this,
                        }"
                      />
                    }
                  </td>
                }
              </tr>
            }
          </tbody>
        </table>
      </div>
    </div>
  `,
  styles: `
    :host {
      margin-bottom: 1rem;
      display: block;
    }

    .table-container {
      position: relative;
    }

    .table-wrapper {
      max-height: calc(100dvh - 16rem);
      min-height: 350px;
      overflow: auto;
      border: 1px solid var(--border);
      border-radius: var(--border-radius);
      box-shadow: 0 2px 10px rgba(16, 24, 40, 0.06);
    }

    .app-ui-table.loading {
      opacity: 0.6;
      pointer-events: none;
    }

    ::ng-deep .app-ui-table {
      --border-radius: 0.15rem;
    }

    /* ---- Zmienne kolorów ---- */

    ::ng-deep .app-light .app-ui-table {
      --txt: #2e2e2e;
      --muted: #6b7280;
      --border: #e6e6ea;
      --ring: #4dabf7;
      --hover: #eef5ff;
      --bg: #ffffff;
      --bg-head: #f8f9fa;
      --bg-alt: #f8f9fa;
    }

    ::ng-deep .app-dark .app-ui-table {
      --txt: #e6e6e6;
      --muted: #9ca3af;
      --border: #3f3f56;
      --ring: #60a5fa;
      --hover: #2d3748;
      --bg: #1e1e2f;
      --bg-head: #252538;
      --bg-alt: #252538;
    }

    /* ---- Styl tabeli ---- */
    .app-ui-table {
      width: 100%;
      border-collapse: separate;
      border-spacing: 0;
      color: var(--txt);
      background: var(--bg);
      border: none;
      table-layout: fixed;
      min-width: 100%;
    }

    .app-ui-table thead th {
      background: var(--bg-head);
      text-align: left;
      font-weight: 600;
      padding: 12px 14px;
      border-bottom: 1px solid var(--border);
      color: var(--txt);
      letter-spacing: 0.2px;
      position: sticky;
      top: 0;
      z-index: 10;
    }

    .app-ui-table td {
      padding: 10px 14px;
      vertical-align: middle;
      border-bottom: 1px solid var(--border);
      overflow-wrap: anywhere;
    }

    .app-ui-table tbody tr:nth-child(even) td {
      background: var(--bg-alt);
    }
    .app-ui-table tbody tr:hover td {
      background: var(--hover);
    }

    .app-ui-table tbody tr:last-child td {
      border-bottom: 0;
    }

    @media (max-width: 720px) {
      .table-wrapper {
        overflow-x: auto;
      }
      .app-ui-table {
        min-width: 600px;
      }
    }
  `,
})
export class UiTable {
  public readonly data = input<any[]>([]);
  public readonly loading = input<boolean>(false);
  public readonly columnsContent = contentChildren(UiTableColumn, {
    read: UiTableColumn,
  });
  public readonly columnsInput = input<readonly UiTableColumn[]>([]);
  public readonly columns = computed(() =>
    this.columnsContent().concat(this.columnsInput()),
  );
  public readonly trackBy = input<Function | null>(null as any);

  public readonly hasFilter = computed(
    () => this.columns().filter((e) => e.filterTemplate()).length > 0,
  );

  public readonly _revision = signal<number>(+new Date());
  public readonly revision = this._revision.asReadonly();
  private readonly _refreshing = signal(false);
  public readonly refreshing = this._refreshing.asReadonly();

  public readonly renderedColumns = computed(() => {
    this.data();
    return this.columns();
  });

  public refresh(): void {
    this._revision.set(+new Date());
  }
}
