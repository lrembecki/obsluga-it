import { NgTemplateOutlet } from '@angular/common';
import { Component, computed, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DataTableColumnSchema } from './data-table.types';

@Component({
  selector: 'app-data-table-render-link',
  imports: [RouterLink, NgTemplateOutlet],
  template: `
    <ng-template #contentTemplate>
      <ng-content />
    </ng-template>

    @if (this.absoluteLink()) {
      <a [href]="this.absoluteLink()!.url" target="_blank" rel="noopener">
        <ng-container *ngTemplateOutlet="contentTemplate" />
      </a>
    } @else {
      <a [routerLink]="this.routeLink()">
        <ng-container *ngTemplateOutlet="contentTemplate" />
      </a>
    }
  `,
  styles: ``,
})
export class DataTableRenderLink {
  readonly column = input.required<DataTableColumnSchema<any>>();
  readonly row = input.required<unknown>();
  readonly renderedLink = computed(() => this.column().renderLink!(this.row()));
  readonly routeLink = computed(() => this.renderedLink() as string[]);
  readonly absoluteLink = computed(() => {
    const link = this.renderedLink();
    return typeof link === 'object' && 'url' in link ? link : null;
  });
}
