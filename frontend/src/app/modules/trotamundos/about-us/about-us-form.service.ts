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
import { AboutUsItemVM, AboutUsPersonVM, AboutUsVM } from './about-us.vm';

export class TrotamundosAboutUsFormService extends ArrayFormService<AboutUsVM> {
  constructor() {
    super();

    this._returnRoute.set(['/modules/trotamundos/about-us/']);

    effect(() => {
      this._schema.set(
        new FormSchema<AboutUsVM>({
          patchValue: {
            items: [],
          },
          fields: [
            new TextFormFieldSchema<AboutUsVM>({
              label: 'Title',
              key: 'title',
              validators: [Validators.required],
              colClass: 'col-6',
            }),
            new TextareaFormFieldSchema<AboutUsVM>({
              label: 'Description',
              key: 'description',
              rows: 6,
              colClass: 'col-12',
            }),
            new TextareaFormFieldSchema<AboutUsVM>({
              label: 'Footer description',
              key: 'footerDescription',
              rows: 6,
              colClass: 'col-12',
            }),
            new TextFormFieldSchema<AboutUsVM>({
              label: 'Footer highlight',
              key: 'footerHighlight',
              colClass: 'col-12',
            }),
            new TextFormFieldSchema<AboutUsVM>({
              key: 'imageId',
              isVisible: false,
            }),
            new ImageFormFieldSchema<AboutUsVM>({
              label: 'Image',
              key: 'image',
              colClass: 'col-12',
              editable: true,
            }),
            new CollectionFormFieldSchema<AboutUsVM, AboutUsItemVM>({
              label: 'Items',
              key: 'items',
              orderField: 'order',
              addButtonText: 'Add item',
              emptyText: 'No items',
              itemColClass: 'col-12',
              layout: 'vertical',
              itemFields: [
                new TextFormFieldSchema<AboutUsItemVM>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
                new TextFormFieldSchema<AboutUsItemVM>({
                  key: 'icon' as any,
                  label: 'Icon',
                  colClass: 'col-3',
                }),
                new TextFormFieldSchema<AboutUsItemVM>({
                  key: 'title' as any,
                  label: 'Item Title',
                  validators: [Validators.required],
                  colClass: 'col-12',
                }),
                new TextareaFormFieldSchema<AboutUsItemVM>({
                  key: 'description' as any,
                  label: 'Item Description',
                  rows: 3,
                  colClass: 'col-12',
                }),
              ],
            }),
            new CollectionFormFieldSchema<AboutUsVM, AboutUsPersonVM>({
              label: 'Persons',
              key: 'persons',
              addButtonText: 'Add person',
              emptyText: 'No persons',
              itemColClass: 'col-12',
              layout: 'vertical',
              orderField: 'order',
              itemFields: [
                new TextFormFieldSchema<AboutUsPersonVM>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
                new TextFormFieldSchema<AboutUsPersonVM>({
                  key: 'name' as any,
                  label: 'Name',
                  validators: [Validators.required],
                  colClass: 'col-12',
                }),
                new TextareaFormFieldSchema<AboutUsPersonVM>({
                  key: 'description' as any,
                  label: 'Description',
                  rows: 3,
                  colClass: 'col-12',
                }),
                new TextFormFieldSchema<AboutUsPersonVM>({
                  key: 'imageId' as any,
                  isVisible: false,
                }),
                new ImageFormFieldSchema<AboutUsPersonVM>({
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
