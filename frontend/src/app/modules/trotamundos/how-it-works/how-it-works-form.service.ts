import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  CollectionFormFieldSchema,
  FormSchema,
  ImageFormFieldSchema,
  TextareaFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { ArrayFormService } from '@app/shared/forms/form.service';
import { HowItWorksItemVM, HowItWorksVM } from './how-it-works.vm';

export class TrotamundosHowItWorksFormService extends ArrayFormService<HowItWorksVM> {
  constructor() {
    super();

    this._returnRoute.set(['/modules/trotamundos/how-it-works/']);

    effect(() => {
      this._schema.set(
        new FormSchema<HowItWorksVM>({
          patchValue: {
            items: [],
          },
          fields: [
            new TextFormFieldSchema<HowItWorksVM>({
              label: 'Title',
              key: 'title',
              validators: [Validators.required],
              colClass: 'col-6',
            }),
            new TextareaFormFieldSchema<HowItWorksVM>({
              label: 'Header Text',
              key: 'headerText',
              rows: 4,
              colClass: 'col-12',
            }),
            new TextareaFormFieldSchema<HowItWorksVM>({
              label: 'Footer Text',
              key: 'footerText',
              rows: 4,
              colClass: 'col-12',
            }),
            new CollectionFormFieldSchema<HowItWorksVM, HowItWorksItemVM>({
              label: 'Items',
              key: 'items',
              orderField: 'order',
              addButtonText: 'Add item',
              emptyText: 'No items',
              itemColClass: 'col-12',
              layout: 'vertical',
              itemFields: [
                new TextFormFieldSchema<HowItWorksItemVM>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
                new TextFormFieldSchema<HowItWorksItemVM>({
                  key: 'title' as any,
                  label: 'Item Title',
                  validators: [Validators.required],
                  colClass: 'col-12',
                }),
                new TextareaFormFieldSchema<HowItWorksItemVM>({
                  key: 'description' as any,
                  label: 'Item Description',
                  rows: 3,
                  colClass: 'col-12',
                }),
                new TextFormFieldSchema<HowItWorksItemVM>({
                  key: 'imageId' as any,
                  isVisible: false,
                }),
                new ImageFormFieldSchema<HowItWorksItemVM>({
                  key: 'image' as any,
                  label: 'Image',
                  colClass: 'col-12',
                  editable: true,
                }),
              ],
            }),
          ],
        }),
      );
    });
  }
}
