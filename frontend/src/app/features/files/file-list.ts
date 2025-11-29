import { Component, computed, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { FileFacade } from 'app/features/files/file.facade';
import { DataTable } from 'app/shared/ui/data-table/data-table';
import { TableColumnConfig } from 'app/shared/ui/data-table/data-table.types';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { Upload } from 'app/shared/ui/upload/upload';
import { GroupFacade } from '../groups/group.facade';
import { FileModel } from './file.model';

@Component({
  selector: 'app-file-list',
  imports: [
    DataTable,
    TranslatePipe,
    Upload,
    UiPanel,
  ],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-upload
          endpoint="/files"
          (uploaded)="_router.navigate(['/files', $event.fileId])"
        />
      </ng-template>
    </app-ui-panel>

    <app-data-table
      [data]="data"
      [columns]="columns"
      [features]="{ quicksearch: true, sortable: true }"
      [loading]="loading()"
      [persistenceKey]="'features-files'"
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
  private readonly _files = inject(FileFacade);
  private readonly _groups = inject(GroupFacade);
  protected readonly _router = inject(Router);
  protected readonly data = this._files.data;
  protected readonly groups = this._groups.data;

  protected readonly saving = signal(false);
  protected readonly loading = computed(
    () => this._files.loading() || this.saving() || this._groups.loading(),
  );

  protected readonly columns: TableColumnConfig<FileModel>[] = [
    { field: 'name', label: 'Name', type: 'text', width: '250px', sortable: true },
    { field: 'position', label: 'Position', type: 'number', width: '90px', sortable: true },
    { field: 'displayName', label: 'Display Name', type: 'text', width: '450px', sortable: true }
  ];

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._files.filter({ sort });
  }

  protected onSearch(query: string) {
    this._files.filter({ q: query });
  }
}
