import { Component, computed, input } from '@angular/core';
import { fieldValue } from 'app/core/helpers/field.helper';
import { UiTable } from './ui-table';
import { UiTableColumn } from './ui-table-column';

@Component({
  selector: 'app-ui-table-column-cell-template',
  imports: [],
  template: ` {{ renderedValue() }} `,
  styles: ``,
})
export class UiTableColumnCellTemplate {
  public readonly table = input.required<UiTable>();
  public readonly column = input.required<UiTableColumn>();
  public readonly record = input.required<any>();

  public readonly renderedValue = computed(() => {
    this.table().revision();
    return fieldValue(this.record(), this.column().field())?.toString();
  });
}
