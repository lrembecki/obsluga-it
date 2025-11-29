import { Validators } from '@angular/forms';
import {
  CheckboxFormFieldSchema,
  DateFormFieldSchema,
  FormSchema,
  TextareaFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms/form-schema.model';
import { FormService } from '@app/shared/forms/form.service';
import { TripVM } from './trip.vm';

export class TrotamundosTripFormService extends FormService<TripVM> {
  constructor() {
    super();

    this._schema.set(
      new FormSchema<TripVM>({
        fields: [
          new TextFormFieldSchema<TripVM>({
            label: 'Name',
            key: 'name',
            validators: [Validators.required],
          }),
          new CheckboxFormFieldSchema<TripVM>({
            label: 'Active',
            key: 'isActive',
          }),
          new CheckboxFormFieldSchema<TripVM>({
            label: 'Disabled',
            key: 'isDisabled',
          }),
          new TextFormFieldSchema<TripVM>({
            label: 'Title',
            key: 'title',
            validators: [Validators.required],
          }),
          new TextFormFieldSchema<TripVM>({
            label: 'Subtitle',
            key: 'subtitle',
            validators: [],
          }),
          new TextareaFormFieldSchema<TripVM>({
            label: 'Description',
            key: 'description',
            validators: [],
          }),
          new DateFormFieldSchema<TripVM>({
            label: 'Start Date',
            key: 'startDate',
            validators: [],
          }),
          new DateFormFieldSchema<TripVM>({
            label: 'End Date',
            key: 'endDate',
            validators: [],
          }),
          new TextFormFieldSchema<TripVM>({
            label: 'Calendar',
            key: 'calendar',
            validators: [],
          }),
        ],
      }),
    );
  }
}
