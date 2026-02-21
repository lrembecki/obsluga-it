import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, TextareaFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { LoyalityProgramVM } from './loyality-program.vm';

export class TrotamundosLoyalityProgramFormService extends ArrayFormService<LoyalityProgramVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/trotamundos/loyality-program/list']);
    effect(() => {
      this._schema.set(new FormSchema<LoyalityProgramVM>({
        fields: [
          new TextFormFieldSchema<LoyalityProgramVM>({ key: 'name', label: 'Name', validators: [Validators.required] }),
          new TextFormFieldSchema<LoyalityProgramVM>({ key: 'title', label: 'Title', validators: [Validators.required] }),
          new TextareaFormFieldSchema<LoyalityProgramVM>({ key: 'description', label: 'Description' }),
        ],
      }));
    });
  }
}
