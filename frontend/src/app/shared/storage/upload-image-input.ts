import { CommonModule } from '@angular/common';
import { Component, inject, input, model, output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Valid } from 'app/core/directives/valid';
import { HostControlDirective } from 'app/shared/ui/inputs/host-control.directive';
import { FileSelectEvent, FileUpload } from 'primeng/fileupload';
import { convertImageFileToStorageVM } from './storage.helper';
import { StorageVM } from './storage.vm';

@Component({
  selector: 'app-upload-image-input',
  imports: [ReactiveFormsModule, FormsModule, CommonModule, FileUpload],
  template: `
    <p-fileupload #fu mode="basic" chooseLabel="Choose" chooseIcon="pi pi-upload" accept="image/*" (onSelect)="uploadHandler($event)" />
  `,
  styles: `
    .app-upload-image-input-buttons {
      display: flex;
      gap: 0.5rem;
      margin-bottom: 0.5rem;
    }
    :host {
      display: flex;
      flex-direction: column;
      gap: 0.25rem;
    }

    img {
      max-height: 30px;
      max-width: 30px;
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

  protected async uploadHandler(event: FileSelectEvent) {

    const storageVM: StorageVM = new StorageVM({
      ...this.value() ?? {},
      ...(await convertImageFileToStorageVM(event.currentFiles[0])),
    });

    this.value.set(storageVM);
    this.valueChange.emit(storageVM);
  }
}
