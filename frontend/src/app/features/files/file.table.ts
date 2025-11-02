import { effect, inject, Injectable } from '@angular/core';
import { FileFacade } from 'app/features/files/file.facade';
import { ButtonPanelService } from '../../shared/ui/button-panel/button-panel';
import {
  TableColumnModel,
  TableService,
} from '../../shared/ui/table/table.service';
import { FileModel } from './file.model';

@Injectable()
export class FileButtonPanelService extends ButtonPanelService {
  constructor() {
    super();
    this.data.set([]);
  }
}

@Injectable()
export class FileTableService extends TableService<FileModel> {
  private readonly _facade = inject(FileFacade);

  constructor() {
    super();
    effect(() => this.data.set(this._facade.data()));
    effect(() => this.loading.set(this._facade.loading()));

    this.columns.set([
      new TableColumnModel({
        label: this._translation.instant('UI.NAME'),
        field: 'name',
        navigate: (record: FileModel) => [`${record.fileId}`],
      }),
      new TableColumnModel({
        label: this._translation.instant('UI.DISPLAY_NAME'),
        field: 'displayName',
      }),
    ]);
  }
}
