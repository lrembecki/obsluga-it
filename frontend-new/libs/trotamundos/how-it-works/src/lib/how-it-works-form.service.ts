import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, TextareaFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { HowItWorksVM } from './how-it-works.vm';

export class TrotamundosHowItWorksFormService extends ArrayFormService<HowItWorksVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/trotamundos/how-it-works/list']);
    effect(() => {
      this._schema.set(new FormSchema<HowItWorksVM>({
        fields: [
          new TextFormFieldSchema<HowItWorksVM>({ key: 'title', label: 'Title', validators: [Validators.required] }),
          new TextareaFormFieldSchema<HowItWorksVM>({ key: 'content', label: 'Content' }),
        ],
      }));
    });
  }
}
