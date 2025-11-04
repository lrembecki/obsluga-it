import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { injectSecurityPermissionGroups } from './permission-group.provider';
import { PermissionGroupVM } from './permission-group.vm';

@Component({
  selector: 'app-permission-group-list',
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
    <app-ui-table [data]="_services.groups.data()">
      <app-ui-table-column
        text="Name"
        field="name" width="300px"
        link
        [renderLink]="renderLink"
      />
    </app-ui-table>
  `,
  styles: ``
})
export class PermissionGroupList {
  protected readonly _services = injectSecurityPermissionGroups();

  protected renderLink = (record: PermissionGroupVM) => ['..', record.id];
}
