import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, TextareaFormFieldSchema, DateFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { TripVM } from './trip.vm';

export class TrotamundosTripFormService extends ArrayFormService<TripVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/trotamundos/trips/list']);
    effect(() => {
      this._schema.set(new FormSchema<TripVM>({
        fields: [
          new TextFormFieldSchema<TripVM>({ key: 'name', label: 'Name', validators: [Validators.required] }),
          new TextareaFormFieldSchema<TripVM>({ key: 'description', label: 'Description' }),
          new DateFormFieldSchema<TripVM>({ key: 'startDate', label: 'Start Date' }),
          new DateFormFieldSchema<TripVM>({ key: 'endDate', label: 'End Date' }),
        ],
      }));
    });
  }
}
