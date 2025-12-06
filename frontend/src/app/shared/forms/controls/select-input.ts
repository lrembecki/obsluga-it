import { NgComponentOutlet } from '@angular/common';
import { Component, computed, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { fieldValue } from '@app/core/helpers/field.helper';
import { FormFieldSchema, SelectFormFieldSchema } from '@app/shared/forms';
import { SelectModule } from 'primeng/select';

@Component({
  selector: 'app-select-input',
  imports: [ReactiveFormsModule, SelectModule, NgComponentOutlet],
  template: `
    <label>{{ selectField().label }}</label>
    <p-select
      [options]="selectField().options"
      [formControl]="$any(form().get(selectField().key))"
      optionLabel="label"
      optionValue="value"
      [placeholder]="selectField().placeholder"
      [showClear]="selectField().clearable"
      class="w-full md:w-56"
    >
      @if (selectField().itemTemplate) {
        <ng-template #selectedItem let-selectedOption>
          @if (selectedOption) {
            <div class="flex items-center gap-2">
              <ng-container
                [ngComponentOutlet]="selectField().itemTemplate!"
                [ngComponentOutletInputs]="{ item: selectedOption }"
              />
            </div>
          } @else {
            {{ renderValue(selectedOption, 'label') }}
          }
        </ng-template>

        <ng-template let-item #item>
          <div class="flex items-center gap-2">
            <ng-container
              [ngComponentOutlet]="selectField().itemTemplate!"
              [ngComponentOutletInputs]="{ item: selectedItem }"
            />
          </div>
        </ng-template>
      }
    </p-select>
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
export class SelectInput {
  field = input.required<FormFieldSchema<unknown>>();
  protected selectField = computed(
    () => this.field() as SelectFormFieldSchema<unknown>,
  );
  form = input.required<FormGroup>();

  renderValue = fieldValue;
}
