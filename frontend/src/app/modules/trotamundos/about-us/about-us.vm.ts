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

export class AboutUsPersonVM {
  order: number = null!;
  name: string = null!;
  description: string = null!;
  imageId: string = null!;
  image: StorageVM = new StorageVM();

  constructor(init?: Partial<AboutUsPersonVM>) {
    Object.assign(this, init);
    this.image = new StorageVM(init?.image);
  }
}
export class AboutUsVM {
  id: string = null!;
  title: string = null!;
  description: string = null!;
  footerDescription: string = null!;
  footerHighlight: string = null!;
  imageId: string = null!;
  image: StorageVM = new StorageVM();
  items: AboutUsItemVM[] = [];
  persons: AboutUsPersonVM[] = [];

  constructor(init?: Partial<AboutUsVM>) {
    Object.assign(this, init);
    this.image = new StorageVM(init?.image);
    this.items = init?.items?.map((item) => new AboutUsItemVM(item)) ?? [];
    this.persons =
      init?.persons?.map((person) => new AboutUsPersonVM(person)) ?? [];
  }
}
