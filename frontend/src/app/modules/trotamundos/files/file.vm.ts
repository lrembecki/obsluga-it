import { StorageVM } from 'app/shared/storage/storage.vm';

export class FileVM {
    id: string = null!; // Guid
    group: string = null!;
    storage: StorageVM = null!;
    displayName: string = null!;
    description: string = null!;
    position: number = 0;

    constructor(init?: Partial<FileVM>) {
        Object.assign(this, init);
    }
}
