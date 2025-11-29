import { Validators } from '@angular/forms';
import {
  FormSchema,
  TextFormFieldSchema,
  TextareaFormFieldSchema,
} from '@app/shared/forms/form-schema.model';
import { FormService } from '@app/shared/forms/form.service';
import { LoyalityProgramVM } from './loyality-program.vm';

export class TrotamundosLoyalityProgramFormService extends FormService<LoyalityProgramVM> {
  constructor() {
    super();

    this._schema.set(
      new FormSchema<LoyalityProgramVM>({
        layout: 'two-column',
        fields: [
          new TextFormFieldSchema({
            key: 'name',
            label: 'Name',
            validators: [Validators.required],
          }),
          new TextFormFieldSchema({
            key: 'title',
            label: 'Title',
            validators: [Validators.required],
          }),
          new TextareaFormFieldSchema({
            key: 'description',
            label: 'Description',
            validators: [Validators.required],
          }),
        ],
      }),
    );
  }
}
