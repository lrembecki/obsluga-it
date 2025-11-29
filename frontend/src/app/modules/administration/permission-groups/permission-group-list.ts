import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { DataTable } from 'app/shared/ui/data-table/data-table';
import { TableColumnConfig } from 'app/shared/ui/data-table/data-table.types';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectSecurityPermissionGroups } from './permission-group.provider';
import { PermissionGroupVM } from './permission-group.vm';

@Component({
  selector: 'app-permission-group-list',
  imports: [UiPanel, DataTable, RouterLink, Button, TranslatePipe],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-data-table
      [data]="_services.groups.data"
      [columns]="columns"
      [features]="{ quicksearch: true, sortable: true }"
      [searchPlaceholder]="'DATA_TABLE.SEARCH_PLACEHOLDER' | translate"
      [actionsLabel]="'DATA_TABLE.ACTIONS' | translate"
      [editLabel]="'DATA_TABLE.EDIT' | translate"
      [saveLabel]="'DATA_TABLE.SAVE' | translate"
      [cancelLabel]="'DATA_TABLE.CANCEL' | translate"
      [persistenceKey]="'permission-groups'"
      (orderBy)="onOrderBy($event)"
      (searchQuery)="onSearch($event)"
    />
  `,
  styles: ``,
})
export class PermissionGroupList {
  protected readonly _services = injectSecurityPermissionGroups();
  protected readonly columns: TableColumnConfig<PermissionGroupVM>[] = [
    { field: 'name', label: 'Name', type: 'text', sortable: true },
  ];

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    // Optionally delegate to facade filter
    this._services.groups.filter({ sort });
  }

  protected onSearch(query: string) {
    this._services.groups.filter({ q: query });
  }
}
