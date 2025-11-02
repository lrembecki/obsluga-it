import { Component, computed, Directive, inject, input } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UiTableColumnBase } from './ui-table-column-base';
import { UiTableColumnCellTemplate } from './ui-table-column-cell-template';

@Component({
  selector: 'app-ui-table-column-link-cell-template',
  imports: [UiTableColumnCellTemplate, RouterLink],
  template: `
    <a [routerLink]="routerLink()">
      <app-ui-table-column-cell-template
        [column]="column()"
        [record]="record()"
        [table]="table()"
      />
    </a>
  `,
  styles: ``,
})
export class UiTableColumnLinkCellTemplate extends UiTableColumnCellTemplate {
  protected readonly _router = inject(Router);
  public readonly directive = computed(
    () => this.column().directive() as UiTableColumnLink<unknown>,
  );

  public readonly routerLink = computed(() => {
    const routerLink = this.directive().renderLink()(this.record());

    return routerLink;
  });
}

@Directive({
  selector: 'app-ui-table-column[link]',
})
export class UiTableColumnLink<T> extends UiTableColumnBase<T> {
  public readonly link = input<string>(null!);
  public readonly renderLink = input<(record: T) => string[]>(null!);

  constructor() {
    super(UiTableColumnLinkCellTemplate);
  }
}
