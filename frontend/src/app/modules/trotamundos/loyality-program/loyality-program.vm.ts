import { StorageVM } from 'app/shared/storage/storage.vm';

export * from 'app/shared/storage/storage.vm';

export class LoyalityProgramVM {
    id: string = null!;
    imageId: string = null!; // Guid referencing StorageVM
    image: StorageVM = null!;
    title: string = null!;
    name: string = null!;
    description: string = null!;

    constructor(init?: Partial<LoyalityProgramVM>) {
        Object.assign(this, init);
    }
}
