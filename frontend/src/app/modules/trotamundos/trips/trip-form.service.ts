import { effect, inject } from '@angular/core';
import { Validators } from '@angular/forms';
import { ImageFacade } from '@app/core/facades/image.facade';
import {
  CheckboxFormFieldSchema,
  CollectionFormFieldSchema,
  DateFormFieldSchema,
  FormSchema,
  ImageFormFieldSchema,
  MultiSelectFormFieldSchema,
  SelectFormFieldSchema,
  TextareaFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms/form-schema.model';
import { FormService } from '@app/shared/forms/form.service';
import { TrotamundosAdvantageFacade } from '../advantages/advantage.facade';
import { TrotamundosHighlightFacade } from '../highlights/highlight.facade';
import { TrotamundosTripFacade } from './trip.provider';
import { TripVM } from './trip.vm';

export class TrotamundosTripFormService extends FormService<TripVM> {
  private readonly _facades = {
    trips: inject(TrotamundosTripFacade),
    advantages: inject(TrotamundosAdvantageFacade),
    highlights: inject(TrotamundosHighlightFacade),
    images: inject(ImageFacade),
  };

  constructor() {
    super();

    this._returnRoute.set(['/modules/trotamundos/trips/list']);

    effect(() => {
      this._schema.set(
        new FormSchema<TripVM>({
          fields: [
            new TextFormFieldSchema<TripVM>({
              label: 'Name',
              key: 'name',
              validators: [Validators.required],
              colClass: 'col-3',
            }),
            new CheckboxFormFieldSchema<TripVM>({
              label: 'Active',
              key: 'isActive',
              colClass: 'col-1',
            }),
            new CheckboxFormFieldSchema<TripVM>({
              label: 'Disabled',
              key: 'isDisabled',
              colClass: 'col-1',
            }),
            new TextFormFieldSchema<TripVM>({
              label: 'Title',
              key: 'title',
              validators: [Validators.required],
              colClass: 'col-3',
            }),
            new TextFormFieldSchema<TripVM>({
              label: 'Subtitle',
              key: 'subtitle',
              validators: [],
              colClass: 'col-3',
            }),
            new TextareaFormFieldSchema<TripVM>({
              label: 'Description',
              key: 'description',
              validators: [],
              colClass: 'col-12',
            }),
            new DateFormFieldSchema<TripVM>({
              label: 'Start Date',
              key: 'startDate',
              validators: [],
              colClass: 'col-3',
            }),
            new DateFormFieldSchema<TripVM>({
              label: 'End Date',
              key: 'endDate',
              validators: [],
              colClass: 'col-3',
            }),
            new TextFormFieldSchema<TripVM>({
              label: 'Calendar',
              key: 'calendar',
              validators: [],
              colClass: 'col-3',
            }),

            // MultiSelect: advantages (string[])
            new MultiSelectFormFieldSchema<TripVM>({
              label: 'Advantages',
              key: 'advantages',
              placeholder: 'Select advantages',
              clearable: true,
              colClass: 'col-12',
              options: this._facades.advantages
                .data()
                .map((e) => ({ label: e.title, value: e.id })),
            }),

            // Collection: agenda (order:number, content:string)
            new CollectionFormFieldSchema<
              TripVM,
              { order: number; content: string }
            >({
              label: 'Agenda',
              key: 'agenda',
              orderField: 'order',
              addButtonText: 'Add item',
              emptyText: 'No items',
              itemColClass: 'col-12',
              itemFields: [
                new TextFormFieldSchema<{ order: number; content: string }>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
                new TextareaFormFieldSchema<{ order: number; content: string }>(
                  {
                    key: 'content' as any,
                    label: 'Content',
                    colClass: 'col-11',
                  },
                ),
              ],
            }),

            // Collection: highlights (order:number, highlightId:string, value:string)
            new CollectionFormFieldSchema<
              TripVM,
              { order: number; highlightId: string; value: string }
            >({
              label: 'Highlights',
              key: 'highlights',
              orderField: 'order',
              addButtonText: 'Add highlight',
              emptyText: 'No highlights',
              itemColClass: 'col-12',
              itemFields: [
                new SelectFormFieldSchema<{
                  order: number;
                  highlightId: string;
                  value: string;
                }>({
                  key: 'highlightId' as any,
                  label: 'Highlight',
                  placeholder: 'Select highlight',
                  colClass: 'col-4',
                  options: this._facades.highlights
                    .data()
                    .map((e) => ({ label: e.title, value: e.id })),
                }),
                new TextFormFieldSchema<{
                  order: number;
                  highlightId: string;
                  value: string;
                }>({
                  key: 'value' as any,
                  label: 'Value',
                  colClass: 'col-7',
                }),
                new TextFormFieldSchema<{
                  order: number;
                  highlightId: string;
                  value: string;
                }>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
              ],
            }),

            // Collection: images (order:number, imageId:string, image:StorageVM)
            new CollectionFormFieldSchema<
              TripVM,
              { order: number; imageId: string; image: any }
            >({
              label: 'Images',
              layout: 'horizontal',
              key: 'images',
              orderField: 'order',
              addButtonText: 'Add image',
              emptyText: 'No images',
              itemColClass: 'col-12',
              itemFields: [
                new ImageFormFieldSchema<{
                  order: number;
                  imageId: string;
                  image: any;
                }>({
                  key: 'image' as any,
                  label: 'Image',
                  colClass: 'col-10',
                  validators: [Validators.required],
                }),
                new TextFormFieldSchema<{
                  order: number;
                  imageId: string;
                  image: any;
                }>({
                  key: 'imageId' as any,
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-2',
                }),
                new TextFormFieldSchema<{
                  order: number;
                  imageId: string;
                  image: any;
                }>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-2',
                }),
              ],
            }),
          ],
        }),
      );
    });
  }
}
