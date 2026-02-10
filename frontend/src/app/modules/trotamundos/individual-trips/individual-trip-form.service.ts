import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  CollectionFormFieldSchema,
  FormSchema,
  ImageFormFieldSchema,
  NumberFormFieldSchema,
  TextareaFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { ArrayFormService } from '@app/shared/forms/form.service';
import {
  IndividualTripItemVM,
  IndividualTripStepItemVM,
  IndividualTripVM,
} from './individual-trip.vm';

export class TrotamundosIndividualTripFormService extends ArrayFormService<IndividualTripVM> {
  constructor() {
    super();

    this._returnRoute.set(['/modules/trotamundos/individual-trips/']);

    effect(() => {
      this._schema.set(
        new FormSchema<IndividualTripVM>({
          patchValue: {
            items: [],
            steps: [],
          },
          fields: [
            new TextFormFieldSchema<IndividualTripVM>({
              label: 'Title',
              key: 'title',
              validators: [Validators.required, Validators.maxLength(250)],
              colClass: 'col-12',
            }),
            new TextareaFormFieldSchema<IndividualTripVM>({
              label: 'Description',
              key: 'description',
              validators: [Validators.required, Validators.maxLength(5000)],
              rows: 6,
              colClass: 'col-12',
            }),
            new CollectionFormFieldSchema<
              IndividualTripVM,
              IndividualTripItemVM
            >({
              label: 'Pricing Options',
              key: 'items',
              orderField: 'order',
              addButtonText: 'Add pricing option',
              emptyText: 'No pricing options',
              itemColClass: 'col-12',
              layout: 'vertical',
              itemFields: [
                new TextFormFieldSchema<IndividualTripItemVM>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
                new TextFormFieldSchema<IndividualTripItemVM>({
                  key: 'title' as any,
                  label: 'Title',
                  validators: [Validators.required, Validators.maxLength(250)],
                  colClass: 'col-12',
                }),
                new TextareaFormFieldSchema<IndividualTripItemVM>({
                  key: 'description' as any,
                  label: 'Description',
                  validators: [Validators.required, Validators.maxLength(5000)],
                  rows: 3,
                  colClass: 'col-12',
                }),
                new NumberFormFieldSchema<IndividualTripItemVM>({
                  key: 'price' as any,
                  label: 'Price',
                  validators: [Validators.required, Validators.min(0)],
                  colClass: 'col-6',
                }),
                new TextFormFieldSchema<IndividualTripItemVM>({
                  key: 'uom' as any,
                  label: 'Unit of Measure',
                  validators: [Validators.required, Validators.maxLength(50)],
                  colClass: 'col-6',
                  placeholder: 'e.g., per person, per day',
                }),
                new TextFormFieldSchema<IndividualTripItemVM>({
                  key: 'imageId' as any,
                  isVisible: false,
                }),
                new ImageFormFieldSchema<IndividualTripItemVM>({
                  key: 'image' as any,
                  label: 'Image',
                  validators: [Validators.required],
                  colClass: 'col-12',
                  editable: true,
                }),
              ],
            }),
            new CollectionFormFieldSchema<
              IndividualTripVM,
              IndividualTripStepItemVM
            >({
              label: 'Process Steps',
              key: 'steps',
              addButtonText: 'Add step',
              emptyText: 'No steps',
              itemColClass: 'col-12',
              layout: 'vertical',
              orderField: 'order',
              itemFields: [
                new TextFormFieldSchema<IndividualTripStepItemVM>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
                new TextFormFieldSchema<IndividualTripStepItemVM>({
                  key: 'title' as any,
                  label: 'Title',
                  validators: [Validators.required, Validators.maxLength(250)],
                  colClass: 'col-12',
                }),
                new TextareaFormFieldSchema<IndividualTripStepItemVM>({
                  key: 'description' as any,
                  label: 'Description',
                  validators: [Validators.required, Validators.maxLength(5000)],
                  rows: 3,
                  colClass: 'col-12',
                }),
              ],
            }),
          ],
        }),
      );
    });
  }
}
