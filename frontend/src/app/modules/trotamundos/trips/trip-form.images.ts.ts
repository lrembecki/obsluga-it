import { Component, computed, model } from '@angular/core';
import { StorageListInput } from "app/shared/storage/storage-list-input.js";
import { StorageVM } from '../loyality-program/loyality-program.vm';
import { TripContextModel, TripImageDTO } from './trip.dto';

@Component({
  selector: 'app-trip-form-images',
  imports: [StorageListInput],
  template: `
    <h2>Images</h2>
    <app-storage-list-input [data]="images()" (dataChange)="onImagesChange($event)" />
  `,
  styles: ``
})
export class TripFormImagesTs {
  public readonly model = model.required<TripContextModel>();

  protected readonly images = computed(() => this.model().session().images.map(e => e.image!));

  public onImagesChange(images: StorageVM[]): void {
    this.model().session().images = images.map((image, index) =>
      TripImageDTO.create(index, image)
    );
    this.model().update();
  }
}
