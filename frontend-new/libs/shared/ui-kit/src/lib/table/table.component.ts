import { Component, input, output } from '@angular/core';
import { TableModule } from 'primeng/table';

@Component({
  selector: 'ui-table',
  standalone: true,
  imports: [TableModule],
  template: `
    <p-table [value]="rows()" [columns]="columns()">
      <ng-template pTemplate="header" let-columns>
        <tr>
          @for (col of columns; track col.field) {
            <th>{{ col.header }}</th>
          }
        </tr>
      </ng-template>
      <ng-template pTemplate="body" let-row let-columns="columns">
        <tr (click)="rowSelected.emit(row)">
          @for (col of columns; track col.field) {
            <td>{{ row[col.field] }}</td>
          }
        </tr>
      </ng-template>
    </p-table>
  `,
})
export class TableComponent<T extends Record<string, unknown>> {
  readonly rows = input.required<T[]>();
  readonly columns = input.required<{ field: string; header: string }[]>();
  readonly rowSelected = output<T>();
}
