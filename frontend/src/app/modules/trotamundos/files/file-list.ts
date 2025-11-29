import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { DataTable } from 'app/shared/ui/data-table/data-table';
import { TableColumnConfig } from 'app/shared/ui/data-table/data-table.types';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectTrotamundosFiles } from './file.provider';
import { FileVM } from './file.vm';

@Component({
  selector: 'app-file-list',
  imports: [UiPanel, DataTable, RouterLink, Button, TranslatePipe],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-data-table
      [data]="_services.files.data"
      [columns]="columns"
      [features]="{ quicksearch: true, sortable: true }"
      [persistenceKey]="'trotamundos-files'"
      [searchPlaceholder]="'DATA_TABLE.SEARCH_PLACEHOLDER' | translate"
      [actionsLabel]="'DATA_TABLE.ACTIONS' | translate"
      [editLabel]="'DATA_TABLE.EDIT' | translate"
      [saveLabel]="'DATA_TABLE.SAVE' | translate"
      [cancelLabel]="'DATA_TABLE.CANCEL' | translate"
      (orderBy)="onOrderBy($event)"
      (searchQuery)="onSearch($event)"
    />
  `,
  styles: ``,
})
export class FileList {
  protected readonly _services = injectTrotamundosFiles();

  protected readonly columns: TableColumnConfig<FileVM>[] = [
    { field: 'position', label: 'Position', type: 'number', width: '80px', sortable: true },
    { field: 'displayName', label: 'Display Name', type: 'text', width: '300px', sortable: true },
    { field: 'group', label: 'Group', type: 'text', width: '200px', sortable: true },
    { field: 'description', label: 'Description', type: 'text', width: '300px', sortable: true }
  ];

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._services.files.filter({ sort });
  }

  protected onSearch(query: string) {
    this._services.files.filter({ q: query });
  }
}
