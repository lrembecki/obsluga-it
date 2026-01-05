import { StorageVM } from '@app/shared/storage/storage.vm';

export class WebsiteMetaVM {
  title: string = null!;
  description: string = null!;
  keywords: string = null!;
  imageId: string = null!;
  image: StorageVM = new StorageVM();

  constructor(init?: Partial<WebsiteMetaVM>) {
    Object.assign(this, init);
    this.image = new StorageVM(init?.image);
  }
}

export class WebsiteVM {
  id: string = null!;
  title: string = null!;
  companyId: string = null!;
  meta: WebsiteMetaVM = new WebsiteMetaVM();

  constructor(init?: Partial<WebsiteVM>) {
    Object.assign(this, init);
    this.meta = new WebsiteMetaVM(init?.meta);
  }
}
