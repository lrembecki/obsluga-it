import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { injectTrotamundosHighlights } from './highlight.provider';
import { HighlightVM } from './highlight.vm';

@Component({
  selector: 'app-highlight-list',
  imports: [
    UiPanel,
    UiTable,
    UiTableColumn,
    RouterLink,
    UiTableColumnLink,
    Button
  ],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-ui-table [data]="_services.highlights.data()">
      <app-ui-table-column
        text="Title"
        field="title"
        link
        [renderLink]="renderLink"
      />
      <app-ui-table-column text="Icon" field="icon" width="200px" />
    </app-ui-table>
  `,
  styles: ``
})
export class HighlightList {
  protected readonly _services = injectTrotamundosHighlights();

  protected renderLink = (record: HighlightVM) => ['..', record.id];
}
