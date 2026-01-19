import { StorageVM } from '@app/shared/storage/storage.vm';

export class HowItWorksItemVM {
  order: number = null!;
  title: string = null!;
  description: string = null!;
  imageId: string | null = null;
  image: StorageVM | null = null;

  constructor(init?: Partial<HowItWorksItemVM>) {
    Object.assign(this, init);
    this.image = init?.image ? new StorageVM(init.image) : null;
  }
}

export class HowItWorksVM {
  id: string = null!;
  title: string = null!;
  headerText: string = null!;
  footerText: string = null!;
  items: HowItWorksItemVM[] = [];

  constructor(init?: Partial<HowItWorksVM>) {
    Object.assign(this, init);
    this.items = init?.items?.map((item) => new HowItWorksItemVM(item)) ?? [];
  }
}
