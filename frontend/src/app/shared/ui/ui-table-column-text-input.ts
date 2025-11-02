import { Component, Directive } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TextInputComponent } from './inputs/text-input.component';
import { UiTableColumnBase } from './ui-table-column-base';
import { UiTableColumnCellTemplate } from './ui-table-column-cell-template';

@Component({
  selector: 'app-ui-table-column-text-input-cell-template',
  imports: [TextInputComponent, ReactiveFormsModule],
  template: `
    <app-text-input
      [value]="renderedValue()"
      (valueChange)="valueChange($event)"
    />
  `,
  styles: `
    ::ng-deep app-ui-table-column-text-input-cell-template .p-inputtext {
      width: 100%;
    }
  `,
})
export class UiTableColumnTextInputCellTemplate extends UiTableColumnCellTemplate {
  valueChange(event: string | undefined) {
    const emitted = { record: this.record(), value: event };
    this.column().directive().recordChange.emit(emitted);
  }
}

@Directive({
  selector: 'app-ui-table-column[input-text]',
})
export class UiTableColumnTextInput<T> extends UiTableColumnBase<T> {
  constructor() {
    super(UiTableColumnTextInputCellTemplate);
  }
}
