import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { injectTrotamundosLoyalityPrograms } from './loyality-program.provider';
import { LoyalityProgramVM } from './loyality-program.vm';

@Component({
    selector: 'app-loyality-program-list',
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
    <app-ui-table [data]="_services.loyalityPrograms.data()">
      <app-ui-table-column text="Name" field="name"  width="300px"
        link
        [renderLink]="renderLink" />
      <app-ui-table-column
        text="Title"
        field="title" width="300px"
      />
      <app-ui-table-column text="Description" field="description" />
    </app-ui-table>
  `,
    styles: ``
})
export class LoyalityProgramList {
    protected readonly _services = injectTrotamundosLoyalityPrograms();

    protected renderLink = (record: LoyalityProgramVM) => ['..', record.id];
}
