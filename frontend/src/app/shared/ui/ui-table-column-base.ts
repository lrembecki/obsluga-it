import { Directive, inject, output } from '@angular/core';
import { TranslationService } from 'app/core/services/translation.service';
import { UiTableColumn } from './ui-table-column';

@Directive()
export class UiTableColumnBase<T> {
  protected readonly column = inject(UiTableColumn, { self: true });
  protected readonly translation = inject(TranslationService);
  public readonly recordChange = output<{
    record: T;
    value: any | undefined;
  }>();

  constructor(template: any) {
    this.column.directive.set(this);
    this.column.template.set(template);
  }
}
