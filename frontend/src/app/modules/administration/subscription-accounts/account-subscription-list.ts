import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { DataTable } from 'app/shared/ui/data-table/data-table';
import { TableColumnConfig } from 'app/shared/ui/data-table/data-table.types';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectSecuritySubscriptionAccounts } from './account-subscription.provider';
import { AccountSubscriptionVM } from './account-subscription.vm';

@Component({
  selector: 'app-account-subscription-list',
  imports: [UiPanel, DataTable, RouterLink, Button, TranslatePipe],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-data-table
      [data]="_services.subscriptions.data"
      [columns]="columns"
      [features]="{ quicksearch: true, sortable: true }"
      [persistenceKey]="'subscription-accounts'"
      [searchPlaceholder]="'DATA_TABLE.SEARCH_PLACEHOLDER' | translate"
      [actionsLabel]="'DATA_TABLE.ACTIONS' | translate"
      [editLabel]="'DATA_TABLE.EDIT' | translate"
      [saveLabel]="'DATA_TABLE.SAVE' | translate"
      [cancelLabel]="'DATA_TABLE.CANCEL' | translate"
      (orderBy)="onOrderBy($event)"
      (searchQuery)="onSearch($event)"
    />
  `,
  styles: ``
})
export class AccountSubscriptionList {
  protected readonly _services = injectSecuritySubscriptionAccounts();

  protected readonly columns: TableColumnConfig<AccountSubscriptionVM>[] = [
    { field: 'email', label: 'Email', type: 'text', width: '250px', sortable: true },
    { field: 'subscription', label: 'Subscription', type: 'text', width: '250px', sortable: true },
    { field: 'isDefault', label: 'Default', type: 'text', width: '120px', sortable: true },
    { field: 'isActive', label: 'Active', type: 'text', width: '120px', sortable: true },
    {
      field: 'permissionGroups',
      label: 'Groups',
      type: 'text',
      width: '120px',
      render: (record: AccountSubscriptionVM) => record.permissionGroups.length.toString()
    },
    { field: 'id', label: 'ID', type: 'text', width: '120px' }
  ];

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._services.subscriptions.filter({ sort });
  }

  protected onSearch(query: string) {
    this._services.subscriptions.filter({ q: query });
  }
}
