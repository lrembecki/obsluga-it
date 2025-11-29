import { Validators } from '@angular/forms';
import {
  FormSchema,
  TextFormFieldSchema,
  TextareaFormFieldSchema,
} from '@app/shared/forms/form-schema.model';
import { FormService } from '@app/shared/forms/form.service';
import { AdvantageVM } from './advantage.vm';

export class TrotamundosAdvantageFormService extends FormService<AdvantageVM> {
  constructor() {
    super();

    this._schema.set(
      new FormSchema<AdvantageVM>({
        fields: [
          new TextFormFieldSchema({
            key: 'title',
            label: 'Title',
            validators: [Validators.required],
          }),
          new TextareaFormFieldSchema({
            key: 'content',
            label: 'Content',
            validators: [Validators.required],
          }),
        ],
      }),
    );

    this._returnRoute.set(['..']);
  }
}
