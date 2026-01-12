import { StorageVM } from '@app/shared/storage/storage.vm';

export class AboutUsItemVM {
  order: number = null!;
  icon: string = null!;
  title: string = null!;
  description: string = null!;

  constructor(init?: Partial<AboutUsItemVM>) {
    Object.assign(this, init);
  }
}

export class AboutUsVM {
  id: string = null!;
  title: string = null!;
  description: string = null!;
  imageId: string = null!;
  image: StorageVM = new StorageVM();
  items: AboutUsItemVM[] = [];

  constructor(init?: Partial<AboutUsVM>) {
    Object.assign(this, init);
    this.image = new StorageVM(init?.image);
    this.items = init?.items?.map((item) => new AboutUsItemVM(item)) ?? [];
  }
}
