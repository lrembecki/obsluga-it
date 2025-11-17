import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { injectTrotamundosFiles } from './file.provider';
import { FileVM } from './file.vm';

@Component({
    selector: 'app-file-list',
    imports: [UiPanel, UiTable, UiTableColumn, RouterLink, UiTableColumnLink, Button],
    template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-ui-table [data]="_services.files.data()">
        <app-ui-table-column
          text="Position"
          field="position"
          width="80px"
        />
      <app-ui-table-column
        text="Filename"
        field="storage.filename"
        link
        [renderLink]="renderLink"
      />
      <app-ui-table-column
        text="Display Name"
        field="displayName"
        width="300px"
      />
      <app-ui-table-column
        text="Group"
        field="group" width="200px"
      />
      <app-ui-table-column
        text="Description"
        field="description"
        width="300px"
      />
    </app-ui-table>
  `,
    styles: ``,
})
export class FileList {
    protected readonly _services = injectTrotamundosFiles();

    protected renderLink = (record: FileVM) => ['..', record.id];
}
