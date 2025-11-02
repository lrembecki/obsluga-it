import { GroupModel } from '../groups/group.model';

export class FileModel {
  fileId: string = null!;
  fileGroupId: string = null!;
  fileGroup: GroupModel = null!;
  position: number = null!;
  name: string = null!;
  displayName: string = null!;
  description: string = null!;
  blobPath: string = null!;
  url: string = null!;
  fileSize: number = null!;

  constructor(init?: Partial<FileModel>) {
    Object.assign(this, init);

    if (init?.fileGroup) {
      this.fileGroupId = init.fileGroup.groupId;
    }
  }
}

export class FileGroupModel {
  fileGroupId: string = null!;
  name: string = null!;
}
