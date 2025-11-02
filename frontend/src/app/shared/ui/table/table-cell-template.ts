import { Component, model } from '@angular/core';
import { TableColumnModel } from './table.service';

@Component({
  selector: 'app-table-cell-template',
  imports: [],
  template: `
    @if (column().field) {
      {{ column().render(record()) }}
    }
  `,
  styles: ``,
})
export class TableCellTemplate<T> {
  record = model.required<T>();
  column = model.required<TableColumnModel<T>>();
}
