import { Component, model, output } from '@angular/core';
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
          <div class="image-overlay">
            <button type="button" (click)="removeImage(item)">×</button>
          </div>
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
      justify-content: flex-start;
      flex-wrap: wrap;
      gap: .25rem;
      padding: .25rem;
    }

    .image-item {
      position: relative;
      overflow: hidden;
      max-width: 150px;
      max-height: 150px;
      border: 1px solid var(--border-color);
      border-radius: 4px;
      

      .image-overlay {
        display: none;
        position: absolute;
        top: 0;
        right: 0;
        background: rgba(0, 0, 0, 0.5);
        color: white;
        padding: 0.25rem;
        cursor: pointer;
        z-index: 1;
        button {
          background: transparent;
          border: none;
          color: white;
          font-size: 1.25rem;
          line-height: 1;
          cursor: pointer;
        }
      }

      &:hover .image-overlay {
        display: block;
      }


      img {
        max-height: 150px;
        object-fit: cover;
      }
    }
  `
})
export class StorageListInput {
  public readonly data = model<StorageVM[]>([]);
  public readonly imageAdd = output<StorageVM>();
  public readonly remove = output<StorageVM>();

  public onValueChange(value: StorageVM): void {
    this.imageAdd.emit(value);
    this.data.set(this.data().concat([value]));
  }

  public removeImage(image: StorageVM): void {
    this.data.set(this.data().filter(i => i !== image));
    this.remove.emit(image);
  }
}
