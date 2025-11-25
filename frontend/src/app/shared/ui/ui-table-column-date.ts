import { DatePipe } from '@angular/common';
import { Component, Directive, input } from '@angular/core';
import { DateFormat } from 'app/core/defaults/date.default';
import { UiTableColumnBase } from './ui-table-column-base';
import { UiTableColumnCellTemplate } from './ui-table-column-cell-template';

@Directive({
  selector: 'app-ui-table-column[date]',
})
export class UiTableColumnDate<T> extends UiTableColumnBase<T> {
  public readonly format = input<DateFormat>('short');
  constructor() {
    super(UiTableColumnDateCellTemplate);
  }
  ngOnInit() {
    this.column.text.set(this.translation.instant('Date/Time'));
  }
}

@Component({
  selector: 'app-ui-table-column-date-cell-template',
  imports: [DatePipe],
  template: ` {{ renderedValue() | date: column().directive().format()! }} `,
  styles: ``,
})
export class UiTableColumnDateCellTemplate extends UiTableColumnCellTemplate { }
