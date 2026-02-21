import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { HighlightVM } from './highlight.vm';

export class TrotamundosHighlightFormService extends ArrayFormService<HighlightVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/trotamundos/highlights/list']);
    effect(() => {
      this._schema.set(new FormSchema<HighlightVM>({
        fields: [
          new TextFormFieldSchema<HighlightVM>({ key: 'title', label: 'Title', validators: [Validators.required] }),
          new TextFormFieldSchema<HighlightVM>({ key: 'icon', label: 'Icon' }),
        ],
      }));
    });
  }
}
