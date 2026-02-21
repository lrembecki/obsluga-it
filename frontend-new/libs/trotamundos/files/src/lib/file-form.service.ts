import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, TextareaFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { FileVM } from './file.vm';

export class TrotamundosFileFormService extends ArrayFormService<FileVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/trotamundos/files/list']);
    effect(() => {
      this._schema.set(new FormSchema<FileVM>({
        fields: [
          new TextFormFieldSchema<FileVM>({ key: 'name', label: 'Name', validators: [Validators.required] }),
          new TextareaFormFieldSchema<FileVM>({ key: 'description', label: 'Description' }),
        ],
      }));
    });
  }
}
