import { Validators } from '@angular/forms';
import {
  FileUploadFormFieldSchema,
  FormSchema,
  TextareaFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms/form-schema.model';
import { FormService } from '@app/shared/forms/form.service';
import { FileVM } from './file.vm';

export class TrotamundosFileFormService extends FormService<FileVM> {
  constructor() {
    super();

    this._schema.set(
      new FormSchema({
        fields: [
          new TextFormFieldSchema<FileVM>({
            label: 'Order',
            key: 'position',
            validators: [Validators.required],
            colClass: 'col-6',
          }),
          new TextFormFieldSchema<FileVM>({
            label: 'Display Name',
            key: 'displayName',
            validators: [Validators.required],
            colClass: 'col-6',
          }),
          new FileUploadFormFieldSchema<FileVM>({
            label: 'File',
            key: 'storage',
            validators: [Validators.required],
            accept: '*/*',
            maxFileSize: 50000000, // 50MB
            colClass: 'col-12',
          }),
          new TextFormFieldSchema<FileVM>({
            label: 'Group',
            key: 'group',
            validators: [],
            colClass: 'col-6',
          }),
          new TextareaFormFieldSchema<FileVM>({
            label: 'Description',
            key: 'description',
            validators: [],
            colClass: 'col-12',
          }),
        ],
      }),
    );

    this._returnRoute.set(['/modules/trotamundos/files/list']);
  }
}
