import { Validators } from '@angular/forms';
import {
  FormSchema,
  ImageFormFieldSchema,
  TextFormFieldSchema,
  TextareaFormFieldSchema,
} from '@app/shared/forms';
import { ArrayFormService } from '@app/shared/forms/form.service';
import { LoyalityProgramVM } from './loyality-program.vm';

export class TrotamundosLoyalityProgramFormService extends ArrayFormService<LoyalityProgramVM> {
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
          new ImageFormFieldSchema({
            key: 'image',
            label: 'Image',
            validators: [Validators.required],
            editable: true,
          }),
        ],
      }),
    );
  }
}
