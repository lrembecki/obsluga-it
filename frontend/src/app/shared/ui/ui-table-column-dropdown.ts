import { Component, Directive, input } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DropdownInputComponent } from './inputs/dropdown-input.component';
import { UiTableColumnBase } from './ui-table-column-base';
import { UiTableColumnCellTemplate } from './ui-table-column-cell-template';

@Directive({
  selector: 'app-ui-table-column[input-dropdown]',
})
export class UiTableColumnDropdown<T> extends UiTableColumnBase<T> {
  data = input.required<any[]>();

  constructor() {
    super(UiTableColumnDropdownInputCellTemplate);
  }
}

@Component({
  selector: 'app-ui-table-column-dropdown-input-cell-template',
  imports: [DropdownInputComponent, ReactiveFormsModule],
  template: `
    <app-dropdown-input
      [data]="column().directive().data()"
      (valueChange)="valueChange($event)"
    />
  `,
  styles: `
    ::ng-deep app-ui-table-column-dropdown-input-cell-template .p-select {
      width: 100%;
    }
  `,
})
export class UiTableColumnDropdownInputCellTemplate extends UiTableColumnCellTemplate {
  valueChange(event: any | undefined) {
    const emitted = { record: this.record(), value: event };
    console.log({ emitted });
    this.column().directive().recordChange.emit(emitted);
  }
}
