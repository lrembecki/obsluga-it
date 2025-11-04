import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { injectTrotamundosAdvantages } from './advantage.provider';
import { AdvantageVM } from './advantage.vm';

@Component({
  selector: 'app-advantage-list',
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
    <app-ui-table [data]="_services.advantages.data()">
      <app-ui-table-column
        text="Title"
        field="title" width="200px"
        link
        [renderLink]="renderLink"
      />
      <app-ui-table-column text="Content" field="content" />
    </app-ui-table>
  `,
  styles: ``
})
export class AdvantageList {
  protected readonly _services = injectTrotamundosAdvantages();

  protected renderLink = (record: AdvantageVM) => ['..', record.id];
}
