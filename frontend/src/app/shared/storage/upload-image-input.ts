import { CommonModule } from '@angular/common';
import { Component, inject, input, model, output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Valid } from 'app/core/directives/valid';
import { Button } from 'app/shared/ui/button/button';
import { HostControlDirective } from 'app/shared/ui/inputs/host-control.directive';
import { FileSelectEvent, FileUpload } from 'primeng/fileupload';
import { ImageStorageVM, StorageVM } from './storage.vm';

@Component({
  selector: 'app-upload-image-input',
  imports: [ReactiveFormsModule, FormsModule, CommonModule, Button, FileUpload],
  template: `
    <div class="app-upload-image-input-buttons">
      <p-fileupload #fu mode="basic" chooseLabel="Choose" chooseIcon="pi pi-upload" accept="image/*" (onSelect)="uploadHandler($event)" />
      <app-button color="primary" text="Upload Image" (buttonClick)="fu.upload()" />
    </div>
    

    @if (value()) {
      <img [src]="value().binaryData ?? value().blobUrl" [alt]="value().filename" [width]="value().image?.width" [height]="value().image?.height" />
    }
  `,
  styles: `
    .app-upload-image-input-buttons {
      display: flex;
      gap: 0.5rem;
      margin-bottom: 0.5rem;
      justify-content: space-between;
    }
    :host {
      display: flex;
      flex-direction: column;
      gap: 0.25rem;
    }

    img {
      max-height: 300px;
      object-fit: contain;
    }
  `,
  hostDirectives: [
    HostControlDirective,
    {
      directive: Valid,
      inputs: ['valid'],
    },
  ]
})
export class UploadImageInput {
  protected readonly _id = `tb-${crypto.randomUUID()}`;
  protected readonly hcd = inject(HostControlDirective);
  public readonly disabled = input<boolean>(false);
  public readonly value = model<StorageVM>(null!);
  public readonly valueChange = output<StorageVM>();

  protected uploadHandler(event: FileSelectEvent) {
    console.log({ event });

    const file = event.currentFiles[0];

    const reader = new FileReader();
    reader.onload = () => {
      const result = reader.result as string;

      const image = new Image();
      image.onload = () => {
        const storageVM: StorageVM = new StorageVM({
          ...this.value() ?? {},
          binaryData: result,
          filename: file.name,
          size: file.size,
          image: new ImageStorageVM({
            width: image.width,
            height: image.height
          })
        });

        this.value.set(storageVM);
        this.valueChange.emit(storageVM);
      };
      image.src = URL.createObjectURL(file);
    }

    reader.readAsDataURL(file);
  }
}
