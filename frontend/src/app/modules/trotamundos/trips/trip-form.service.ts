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
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import { TrotamundosAdvantageFacade } from '../advantages/advantage.facade';
import { TrotamundosHighlightFacade } from '../highlights/highlight.facade';
import { StorageVM } from '../loyality-program/loyality-program.vm';
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

    this._returnRoute.set(['/modules/trotamundos/trips/']);

    effect(() => {
      const highlights = this._facades.highlights.data();

      this._schema.set(
        new FormSchema<TripVM>({
          patchValue: {
            highlights: highlights.map((e, index) => ({
              order: index + 1,
              highlightId: e.id,
              value: '',
            })),
            schedules: [
              { order: 1, title: 'DZIEŃ 1', content: '' },
              { order: 1, title: 'DZIEŃ 2', content: '' },
              { order: 1, title: 'DZIEŃ 3', content: '' },
              { order: 1, title: 'DZIEŃ 4', content: '' },
            ],
            paymentSchedules: [
              { order: 1, title: '1', price: '', description: '' },
              { order: 1, title: '2', price: '', description: '' },
              { order: 1, title: '3', price: '', description: '' },
            ],
            requirements: [],
            suggestedFlights: [],
          },
          fields: [
            // Name: string
            new TextFormFieldSchema<TripVM>({
              label: 'Name',
              key: 'name',
              validators: [Validators.required],
              colClass: 'col-3',
            }),

            // Active: boolean
            new CheckboxFormFieldSchema<TripVM>({
              label: 'Active',
              key: 'isActive',
              colClass: 'col-1',
              value: true,
            }),

            // Disabled: boolean
            new CheckboxFormFieldSchema<TripVM>({
              label: 'Disabled',
              key: 'isDisabled',
              colClass: 'col-1',
              value: false,
            }),

            // Title: string
            new TextFormFieldSchema<TripVM>({
              label: 'Title',
              key: 'title',
              validators: [Validators.required],
              colClass: 'col-3',
            }),

            // Subtitle: string
            new TextFormFieldSchema<TripVM>({
              label: 'Subtitle',
              key: 'subtitle',
              validators: [],
              colClass: 'col-3',
            }),

            // Description: string
            new TextareaFormFieldSchema<TripVM>({
              label: 'Description',
              key: 'description',
              validators: [],
              colClass: 'col-12',
              rows: 8,
            }),

            // Start Date: Date
            new DateFormFieldSchema<TripVM>({
              label: 'Start Date',
              key: 'startDate',
              validators: [],
              colClass: 'col-3',
              value: null!,
            }),

            // End Date: Date
            new DateFormFieldSchema<TripVM>({
              label: 'End Date',
              key: 'endDate',
              validators: [],
              colClass: 'col-3',
              value: null!,
            }),

            // Callendar: string
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

            // Collection: schedule (order:number, title: string, description: string)
            new CollectionFormFieldSchema<
              TripVM,
              { order: number; title: string; content: string }
            >({
              label: 'Schedule',
              key: 'schedules',
              orderField: 'order',
              addButtonText: 'Add schedule item',
              emptyText: 'No items',
              itemColClass: 'col-12',
              itemFields: [
                new TextFormFieldSchema<{
                  order: number;
                  title: string;
                  content: string;
                }>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
                new TextFormFieldSchema<{
                  order: number;
                  title: string;
                  content: string;
                }>({
                  key: 'title' as any,
                  label: 'Title',
                  colClass: 'col-2',
                }),
                new TextareaFormFieldSchema<{
                  order: number;
                  title: string;
                  content: string;
                }>({
                  key: 'content' as any,
                  label: 'Content',
                  colClass: 'col-10',
                  rows: 7,
                }),
              ],
            }),

            // Collection: price includes (order: number, content: string, includes: boolean)
            new CollectionFormFieldSchema<
              TripVM,
              { order: number; content: string; includes: boolean }
            >({
              label: 'Price Includes',
              key: 'priceIncludes',
              orderField: 'order',
              addButtonText: 'Add item',
              emptyText: 'No items',
              itemColClass: 'col-12',
              itemFields: [
                new TextFormFieldSchema<{
                  order: number;
                  content: string;
                  includes: boolean;
                }>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
                new CheckboxFormFieldSchema<{
                  order: number;
                  content: string;
                  includes: boolean;
                }>({
                  key: 'includes' as any,
                  label: 'Includes',
                  colClass: 'col-1',
                }),
                new TextareaFormFieldSchema<{
                  order: number;
                  content: string;
                  includes: boolean;
                }>({
                  key: 'content' as any,
                  label: 'Content',
                  colClass: 'col-11',
                  rows: 2,
                }),
              ],
            }),

            // Collection: payment schedules (order:number, title:string, price:string, description:string)
            new CollectionFormFieldSchema<
              TripVM,
              {
                order: number;
                title: string;
                price: string;
                description: string;
              }
            >({
              label: 'Payment Schedules',
              key: 'paymentSchedules',
              orderField: 'order',
              addButtonText: 'Add payment schedule',
              emptyText: 'No payment schedules',
              itemColClass: 'col-12',
              itemFields: [
                new TextFormFieldSchema<{
                  order: number;
                  title: string;
                  price: string;
                  description: string;
                }>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
                new TextFormFieldSchema<{
                  order: number;
                  title: string;
                  price: string;
                  description: string;
                }>({
                  key: 'title' as any,
                  label: 'Title',
                  colClass: 'col-3',
                }),
                new TextFormFieldSchema<{
                  order: number;
                  title: string;
                  price: string;
                  description: string;
                }>({
                  key: 'price' as any,
                  label: 'Price',
                  colClass: 'col-2',
                }),
                new TextareaFormFieldSchema<{
                  order: number;
                  title: string;
                  price: string;
                  description: string;
                }>({
                  key: 'description' as any,
                  label: 'Description',
                  colClass: 'col-6',
                  rows: 3,
                }),
              ],
            }),

            // Collection: requirements (order:number, description:string)
            new CollectionFormFieldSchema<
              TripVM,
              { order: number; description: string }
            >({
              label: 'Requirements',
              key: 'requirements',
              orderField: 'order',
              addButtonText: 'Add requirement',
              emptyText: 'No requirements',
              itemColClass: 'col-12',
              itemFields: [
                new TextFormFieldSchema<{
                  order: number;
                  description: string;
                }>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
                }),
                new TextareaFormFieldSchema<{
                  order: number;
                  description: string;
                }>({
                  key: 'description' as any,
                  label: 'Description',
                  colClass: 'col-11',
                  rows: 2,
                }),
              ],
            }),

            // Collection: suggested flights (order:number, imageId:string)
            new TextareaFormFieldSchema<TripVM>({
              label: 'Suggested Flight Notes',
              key: 'suggestedFlightNotes',
            }),
            new CollectionFormFieldSchema<
              TripVM,
              {
                order: number;
                imageId: string;
                image: StorageVM;
              }
            >({
              label: 'Suggested Flights',
              key: 'suggestedFlights',
              orderField: 'order',
              addButtonText: 'Add suggested flight',
              emptyText: 'No suggested flights',
              itemColClass: 'col-12',
              itemFields: [
                new TextFormFieldSchema<{
                  order: number;
                  imageId: string;
                  image: StorageVM;
                }>({
                  key: 'order' as any,
                  label: 'Order',
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-1',
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
                new ImageFormFieldSchema<{
                  order: number;
                  imageId: string;
                  image: StorageVM;
                }>({
                  key: 'image' as any,
                  label: 'Image',
                  colClass: 'col-10',
                  validators: [Validators.required],
                }),
              ],
            }),
          ],
        }),
      );
    });
  }
}
