import { Component, computed, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { StorageVM } from '@app/shared/storage/storage.vm';
import { ButtonModule } from 'primeng/button';
import { FileUploadModule } from 'primeng/fileupload';
import { FormFieldSchema } from '../form-schema.model';

@Component({
  selector: 'app-file-upload-input',
  imports: [ReactiveFormsModule, FileUploadModule, ButtonModule],
  template: `
    <label>{{ field().label }}</label>
    <div class="file-upload-container">
      <p-fileUpload
        mode="basic"
        [name]="String(field().key)"
        [accept]="acceptTypes()"
        [maxFileSize]="maxFileSize()"
        [auto]="true"
        [chooseLabel]="chooseLabel()"
        [customUpload]="true"
        (uploadHandler)="onFileSelect($event)"
        [disabled]="form().get(field().key)?.disabled ?? false"
      />

      @if (currentFile()) {
        <div class="current-file">
          <i class="pi pi-file"></i>
          <span>{{ currentFile()?.filename || 'No file' }}</span>
          @if (currentFile()?.size) {
            <span class="file-size"
              >({{ formatFileSize(currentFile()!.size!) }})</span
            >
          }
        </div>
      }
    </div>
  `,
  host: {
    class: 'input-container',
  },
  styles: `
    :host {
      display: flex;
      flex-direction: column;
      gap: 0.25rem;
    }

    .file-upload-container {
      display: flex;
      flex-direction: column;
      gap: 0.5rem;
    }

    .current-file {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      padding: 0.5rem;
      background: var(--bg-alt);
      border: 1px solid var(--border);
      border-radius: 0.25rem;
      font-size: 0.875rem;

      i {
        color: var(--muted);
      }

      span {
        flex: 1;
      }

      .file-size {
        color: var(--muted);
        font-size: 0.75rem;
      }
    }
  `,
})
export class FileUploadInput {
  protected readonly String = String;

  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();

  protected readonly acceptTypes = computed(() => {
    const fieldSchema = this.field() as any;
    const accept: string | undefined = fieldSchema.accept ?? '*/*';
    // PrimeNG/File input treats empty accept as "all types"; '*/*' can trigger validation error
    return accept === '*/*' ? '' : accept;
  });

  protected readonly maxFileSize = computed(() => {
    const fieldSchema = this.field() as any;
    return fieldSchema.maxFileSize ?? 10000000; // 10MB default
  });

  protected readonly chooseLabel = computed(() => {
    const fieldSchema = this.field() as any;
    return fieldSchema.chooseLabel ?? 'Choose File';
  });

  protected readonly currentFile = computed(() => {
    const control = this.form().get(this.field().key as string);
    return control?.value as StorageVM | null;
  });

  protected async onFileSelect(event: any) {
    const file = event.files[0];
    if (!file) return;

    try {
      const binaryData = await this.fileToBase64(file);
      const storage = new StorageVM({
        id: crypto.randomUUID(),
        filename: file.name,
        size: file.size,
        binaryData: binaryData,
        blobUrl: null,
        blobPath: null,
      });

      const control = this.form().get(this.field().key as string);
      control?.setValue(storage);
      control?.markAsDirty();
    } catch (error) {
      console.error('Error reading file:', error);
    }
  }

  protected removeFile() {
    const control = this.form().get(this.field().key as string);
    control?.setValue(null);
    control?.markAsDirty();
  }

  protected formatFileSize(bytes: number): string {
    if (bytes === 0) return '0 Bytes';
    const k = 1024;
    const sizes = ['Bytes', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return Math.round((bytes / Math.pow(k, i)) * 100) / 100 + ' ' + sizes[i];
  }

  private fileToBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        const result = reader.result as string;
        // Remove data:mime;base64, prefix
        const base64 = result.split(',')[1];
        resolve(base64);
      };
      reader.onerror = () => reject(new Error('Failed to read file'));
    });
  }
}
