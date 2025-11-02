import { Component, computed, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { FileFacade } from 'app/features/files/file.facade';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { Upload } from 'app/shared/ui/upload/upload';
import { GroupFacade } from '../groups/group.facade';
import { FileModel } from './file.model';

@Component({
  selector: 'app-file-list',
  imports: [
    UiTable,
    UiTableColumn,
    TranslatePipe,
    UiTableColumnLink,
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

    <app-ui-table [data]="data()" [loading]="loading()" #table>
      <app-ui-table-column
        [text]="'Name' | translate"
        width="250px"
        field="name"
        link
        [renderLink]="renderRouterLink"
      />
      <app-ui-table-column
        width="200px"
        [text]="'Group' | translate"
        field="fileGroup.name"
      />
      <app-ui-table-column
        width="90px"
        [text]="'Position' | translate"
        field="position"
      />
      <app-ui-table-column
        width="450px"
        [text]="'Display Name' | translate"
        field="displayName"
      />
    </app-ui-table>
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

  protected renderRouterLink = (record: FileModel) => {
    return ['..', record.fileId];
  };
}
