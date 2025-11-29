import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { DataTable } from 'app/shared/ui/data-table/data-table';
import { TableColumnConfig } from 'app/shared/ui/data-table/data-table.types';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectSecurityPermissions } from './permission.provider';
import { PermissionVM } from './permission.vm';

@Component({
  selector: 'app-permission-list',
  imports: [
    UiPanel,
    DataTable,
    RouterLink,
    Button,
    TranslatePipe
  ],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-data-table
      [data]="_services.permissions.data"
      [columns]="columns"
      [features]="{ quicksearch: true, sortable: true }"
      [persistenceKey]="'permissions'"
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
export class PermissionList {
  protected readonly _services = injectSecurityPermissions();

  protected readonly columns: TableColumnConfig<PermissionVM>[] = [
    {
      field: 'name',
      label: 'Name',
      type: 'text',
      width: '300px',
      sortable: true
    }
  ];

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._services.permissions.filter({ sort });
  }

  protected onSearch(query: string) {
    this._services.permissions.filter({ q: query });
  }
}
