import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, TextareaFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { AdvantageVM } from './advantage.vm';

export class TrotamundosAdvantageFormService extends ArrayFormService<AdvantageVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/trotamundos/advantages/list']);
    effect(() => {
      this._schema.set(new FormSchema<AdvantageVM>({
        fields: [
          new TextFormFieldSchema<AdvantageVM>({ key: 'title', label: 'Title', validators: [Validators.required] }),
          new TextareaFormFieldSchema<AdvantageVM>({ key: 'content', label: 'Content' }),
        ],
      }));
    });
  }
}
