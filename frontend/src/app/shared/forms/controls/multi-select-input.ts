import { NgComponentOutlet } from '@angular/common';
import { Component, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MultiSelectModule } from 'primeng/multiselect';
import {
  FormFieldSchema,
  MultiSelectFormFieldSchema,
} from '../form-schema.model';

@Component({
  selector: 'app-multi-select-input',
  standalone: true,
  imports: [ReactiveFormsModule, MultiSelectModule, NgComponentOutlet],
  template: `
    <label>{{ multiField().label }}</label>
    <p-multiSelect
      [options]="multiField().options"
      [formControl]="$any(form().get(multiField().key))"
      optionLabel="label"
      optionValue="value"
      [placeholder]="multiField().placeholder"
      [showClear]="multiField().clearable"
      class="w-full md:w-56"
    >
      @if (multiField().itemTemplate) {
        <ng-template #selectedItems let-value>
          <ng-container
            [ngComponentOutlet]="multiField().itemTemplate!"
            [ngComponentOutletInputs]="{ item: value }"
          />
        </ng-template>
        <ng-template let-item #item>
          <ng-container
            [ngComponentOutlet]="multiField().itemTemplate!"
            [ngComponentOutletInputs]="{ item: item }"
          />
        </ng-template>
      }
    </p-multiSelect>
  `,
  host: {
    class: 'input-container',
  },
  styles: `
    :host {
      display: flex;
      flex-direction: column;
      gap: 0.25rem;
    }
  `,
})
export class MultiSelectInput {
  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();

  protected multiField = () =>
    this.field() as MultiSelectFormFieldSchema<unknown>;
}
