import { CommonModule } from '@angular/common';
import { Component, inject, input, model, output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Valid } from 'app/core/directives/valid';
import { HostControlDirective } from 'app/shared/ui/inputs/host-control.directive';
import { FileSelectEvent, FileUpload } from 'primeng/fileupload';
import { convertFileToStorageVM } from './storage.helper';
import { StorageVM } from './storage.vm';

@Component({
    selector: 'app-upload-file-input',
    imports: [ReactiveFormsModule, FormsModule, CommonModule, FileUpload],
    template: `
    <p-fileupload #fu mode="basic" chooseLabel="Browse" chooseIcon="pi pi-upload" (onSelect)="uploadHandler($event)" />
  `,
    styles: `
    :host { display: block; }
  `,
    hostDirectives: [
        HostControlDirective,
        {
            directive: Valid,
            inputs: ['valid'],
        },
    ]
})
export class UploadFileInput {
    protected readonly _id = `tb-${crypto.randomUUID()}`;
    protected readonly hcd = inject(HostControlDirective);
    public readonly disabled = input<boolean>(false);
    public readonly value = model<StorageVM>(null!);
    public readonly valueChange = output<StorageVM>();

    protected async uploadHandler(event: FileSelectEvent) {
        const file = event.currentFiles[0];

        const storageVM: StorageVM = new StorageVM({
            ...this.value() ?? {},
            ...(await convertFileToStorageVM(file)),
        });

        this.value.set(storageVM);
        this.valueChange.emit(storageVM);
    }
}
