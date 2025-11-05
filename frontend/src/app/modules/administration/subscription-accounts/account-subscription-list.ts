import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { injectSecuritySubscriptionAccounts } from './account-subscription.provider';
import { AccountSubscriptionVM } from './account-subscription.vm';

@Component({
  selector: 'app-account-subscription-list',
  imports: [UiPanel, UiTable, UiTableColumn, UiTableColumnLink, RouterLink, Button],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-ui-table [data]="_services.subscriptions.data()">
      <app-ui-table-column text="Email" field="email" width="250px" />
      <app-ui-table-column text="Subscription" field="subscription" width="250px" />
      <app-ui-table-column text="Default" field="isDefault" width="120px" />
      <app-ui-table-column text="Active" field="isActive" width="120px" />
      <app-ui-table-column text="Groups" field="permissionGroups" width="120px">
        <ng-template #cell let-record="record">
          {{ record.permissionGroups.length }}
        </ng-template>
      </app-ui-table-column>
      <app-ui-table-column text="Open" field="id" link [renderLink]="renderLink" />
    </app-ui-table>
  `,
  styles: ``
})
export class AccountSubscriptionList {
  protected readonly _services = injectSecuritySubscriptionAccounts();
  protected renderLink = (record: AccountSubscriptionVM) => ['..', record.id];
}
