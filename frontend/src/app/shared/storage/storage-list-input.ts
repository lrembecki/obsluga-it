import { Component, model } from '@angular/core';
import { StorageVM } from './storage.vm';
import { UploadImageInput } from "./upload-image-input";

@Component({
  selector: 'app-storage-list-input',
  imports: [UploadImageInput],
  template: `
    <app-upload-image-input #input (valueChange)="onValueChange($event)" />
    <div class="image-collection">
      @for (item of data(); track item) {
        <div class="image-item">
          <img [src]="item.binaryData ?? item.blobUrl" [alt]="item.filename" />
        </div>
      }
    </div>
  `,
  styles: `

    app-upload-image-input {
      margin-bottom: .5rem;
    }

    .image-collection {
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      gap: .25rem;
      overflow-x: auto;
    }

    .image-item {

      position: relative;

      

      img {
        max-width: 200px;
        max-height: 200px;
        object-fit: cover;
        border: 1px solid var(--border-color);
        border-radius: 4px;
      }
    }
  `
})
export class StorageListInput {
  public readonly data = model<StorageVM[]>([]);

  public onValueChange(value: StorageVM): void {
    this.data.set(this.data().concat([value]));
  }
}
