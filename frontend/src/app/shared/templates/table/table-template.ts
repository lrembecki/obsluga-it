import { Component, input, Type } from '@angular/core';
import { DateFormat } from 'app/core/defaults/date.default';
import { UiTable } from "app/shared/ui/ui-table";
import { UiTableColumn } from "app/shared/ui/ui-table-column";
import { UiTableColumnDate } from "app/shared/ui/ui-table-column-date";
import { UiTableColumnLink } from "app/shared/ui/ui-table-column-link";

@Component({
  selector: 'app-table-template',
  imports: [UiTable, UiTableColumn, UiTableColumnLink, UiTableColumnDate],
  template: `
    <app-ui-table [data]="data()" [trackBy]="trackBy()">
      @for (column of this.columns(); track column.trackable) {

        @if (column.link) {
          <app-ui-table-column
            [text]="column.text"
            [field]="column.field"
            [width]="column.width"
            link
            [renderLink]="column.link.renderLink"
          />
        } @else if (column.date) {
          <app-ui-table-column
            [text]="column.text"
            [field]="column.field"
            [width]="column.width"
            date
            [format]="column.date.format"
          />
        } @else {
          <app-ui-table-column [text]="column.text" [field]="column.field" [width]="column.width" />
        }
      }
    </app-ui-table>
  `,
  styles: ``
})
export class TableTemplate<T> {
  public readonly data = input.required<T[]>();
  public readonly columns = input.required<TableColumn<T>[]>();
  public readonly trackBy = input<Function | null>(null as any);
}

export class TableColumn<T> {
  text: string = null!;
  field: string = null!;
  width?: string;

  date?: {
    format: DateFormat;
  }

  link?: {
    // eslint-disable-next-line no-unused-vars
    renderLink: (record: T) => string[];
  }

  /**
   * Optional cell renderer function returning a string (HTML-safe text)
   * or any serializable value used by the table to render a custom cell.
   */
  cell?: Function | null;

  /**
   * Optional component type to render inside the cell. The hosting table
   * / cell should support instantiating the component and providing the
   * `record` as an input.
   */
  component?: Type<any> | null;

  constructor(init?: Partial<TableColumn<T>>) {
    Object.assign(this, init);
  }

  get trackable(): string {
    return [this.text, this.field, this.width].filter(e => e?.length).join('-').trim();
  }

  public static from<T>(init: Partial<TableColumn<T>>): TableColumn<T> {
    return new TableColumn<T>(init);
  }

  public static fromArray<T>(inits: Partial<TableColumn<T>>[]): TableColumn<T>[] {
    return inits.map(init => TableColumn.from<T>(init));
  }
}