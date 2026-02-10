import { StorageVM } from '@app/shared/storage/storage.vm';

export class IndividualTripItemVM {
  order: number = null!;
  title: string = null!;
  description: string = null!;
  price: number = null!;
  uom: string = null!;
  imageId: string = null!;
  image: StorageVM = new StorageVM();

  constructor(init?: Partial<IndividualTripItemVM>) {
    Object.assign(this, init);
    this.image = new StorageVM(init?.image);
  }
}

export class IndividualTripStepItemVM {
  order: number = null!;
  title: string = null!;
  description: string = null!;

  constructor(init?: Partial<IndividualTripStepItemVM>) {
    Object.assign(this, init);
  }
}

export class IndividualTripVM {
  id: string = null!;
  name: string = null!;
  isActive: boolean = false;
  isDisabled: boolean = false;
  subscriptionId: string = null!;
  title: string = null!;
  description: string = null!;
  items: IndividualTripItemVM[] = [];
  steps: IndividualTripStepItemVM[] = [];

  constructor(init?: Partial<IndividualTripVM>) {
    Object.assign(this, init);
    this.items =
      init?.items?.map((item) => new IndividualTripItemVM(item)) ?? [];
    this.steps =
      init?.steps?.map((step) => new IndividualTripStepItemVM(step)) ?? [];
  }
}
