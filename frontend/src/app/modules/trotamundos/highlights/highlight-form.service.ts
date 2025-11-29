import { Validators } from '@angular/forms';
import {
  FormSchema,
  TextFormFieldSchema,
} from '@app/shared/forms/form-schema.model';
import { FormService } from '@app/shared/forms/form.service';
import { HighlightVM } from './highlight.vm';

export class TrotamundosHighlightFormService extends FormService<HighlightVM> {
  constructor() {
    super();

    this._schema.set(
      new FormSchema<HighlightVM>({
        layout: 'two-column',
        fields: [
          new TextFormFieldSchema({
            key: 'title',
            label: 'Title',
            validators: [Validators.required],
          }),
          new TextFormFieldSchema({
            key: 'icon',
            label: 'Icon',
            validators: [Validators.required],
          }),
        ],
      }),
    );

    this._returnRoute.set(['..']);
  }
}
