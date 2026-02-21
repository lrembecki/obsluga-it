import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, TextareaFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { AboutUsVM } from './about-us.vm';

export class TrotamundosAboutUsFormService extends ArrayFormService<AboutUsVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/trotamundos/about-us/list']);
    effect(() => {
      this._schema.set(new FormSchema<AboutUsVM>({
        fields: [
          new TextFormFieldSchema<AboutUsVM>({ key: 'title', label: 'Title', validators: [Validators.required] }),
          new TextareaFormFieldSchema<AboutUsVM>({ key: 'content', label: 'Content' }),
        ],
      }));
    });
  }
}
