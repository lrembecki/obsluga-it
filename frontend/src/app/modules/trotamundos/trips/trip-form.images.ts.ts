import { Component, computed, model } from '@angular/core';
import { StorageListInput } from "app/shared/storage/storage-list-input.js";
import { TripContextModel } from './trip.dto';

@Component({
  selector: 'app-trip-form-images',
  imports: [StorageListInput],
  template: `
    <app-storage-list-input [data]="images()" 
      (imageAdd)="model().addImage($event)" 
      (imageRemove)="model().removeImage(images().indexOf($event))" />
  `,
  styles: ``
})
export class TripFormImagesTs {
  public readonly model = model.required<TripContextModel>();

  protected readonly images = computed(() => this.model().session().images.sort((a, b) => a.order! - b.order!).map(e => e.image!));
}
