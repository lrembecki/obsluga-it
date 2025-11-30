import { CommonModule } from '@angular/common';
import { Component, computed, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { convertImageFileToStorageVM } from '@app/shared/storage/storage.helper';
import { StorageVM } from '@app/shared/storage/storage.vm';
import { FileSelectEvent, FileUpload } from 'primeng/fileupload';
import { FormFieldSchema, ImageFormFieldSchema } from '../form-schema.model';

@Component({
  selector: 'app-image-input',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FileUpload],
  template: `
    <div class="image-input-container">
      <label [for]="fieldId">{{ imageField().label }}</label>

      @if (currentImage; as img) {
        <div class="preview">
          <img
            [src]="img.binaryData || img.blobUrl"
            [alt]="img.filename || 'Image preview'"
          />
        </div>
      } @else {
        <p-fileupload
          #fu
          [id]="fieldId"
          mode="basic"
          chooseLabel="{{ currentImage ? 'Change Image' : 'Choose Image' }}"
          chooseIcon="pi pi-upload"
          [accept]="imageField().accept"
          (onSelect)="uploadHandler($event)"
          [disabled]="imageField().disabled"
        />
      }
    </div>
  `,
  styles: `
    :host {
      display: flex;
      flex-direction: column;
      gap: 0.5rem;
    }
    .image-input-container {
      display: flex;
      flex-direction: column;
      gap: 0.5rem;
    }
    .preview {
      position: relative;
      display: inline-block;
      max-width: 200px;
    }
    .preview img {
      max-width: 100%;
      max-height: 200px;
      object-fit: contain;
      border: 1px solid var(--border);
      border-radius: 4px;
    }
    .remove-btn {
      position: absolute;
      top: 4px;
      right: 4px;
      background: rgba(220, 53, 69, 0.9);
      color: white;
      border: none;
      border-radius: 50%;
      width: 24px;
      height: 24px;
      cursor: pointer;
      display: flex;
      align-items: center;
      justify-content: center;
      padding: 0;
    }
    .remove-btn:hover {
      background: rgba(220, 53, 69, 1);
    }
    label {
      font-weight: 500;
      margin-bottom: 0.25rem;
    }
  `,
})
export class ImageInput {
  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();

  protected imageField = computed(
    () => this.field() as unknown as ImageFormFieldSchema<any>,
  );

  protected fieldId = `image-input-${crypto.randomUUID()}`;

  protected get currentImage(): StorageVM | null {
    const control = this.form().get(this.imageField().key as string);
    return control?.value as StorageVM | null;
  }

  protected async uploadHandler(event: FileSelectEvent): Promise<void> {
    if (!event.currentFiles?.length) return;

    const storageVM = await convertImageFileToStorageVM(event.currentFiles[0]);
    const control = this.form().get(this.imageField().key as string);
    if (control) {
      control.setValue(storageVM);
      control.markAsDirty();
    }
  }
}
