import { inject, Injectable } from '@angular/core';
import { FileModel } from 'app/features/files/file.model';
import { ApiFacade } from '../../core/interfaces/facade.interface';
import { GroupFacade } from '../groups/group.facade';

@Injectable({ providedIn: 'root' })
export class FileFacade extends ApiFacade<FileModel[]> {
  private readonly _groups = inject(FileGroupFacade);

  constructor() {
    super([], 'files');
  }

  protected override withData(data: FileModel[]) {
    data = super.withData(data).map(
      (e) =>
        new FileModel({
          ...e,
          fileGroup: this._groups
            .data()
            .find((y) => y.groupId === e.fileGroupId),
        }),
    );

    return data;
  }
}

@Injectable({ providedIn: 'root' })
export class FileGroupFacade extends GroupFacade {
  constructor() {
    super('file-groups');
  }
}
