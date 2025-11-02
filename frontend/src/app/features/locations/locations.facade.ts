import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { ImageModel } from 'app/core/models/image.model';

export class LocationModel {
  locationId: string = null!;
  name: string = null!;
  description: string = null!;
  images: ImageModel[] = [];

  constructor(init?: Partial<LocationModel>) {
    Object.assign(this, init);
  }
}

export class LocationsFacade extends ApiFacade<LocationModel[]> {
  constructor() {
    super([], 'locations');
  }
}
