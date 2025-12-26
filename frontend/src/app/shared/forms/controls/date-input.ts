import { Component, computed, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DateFormFieldSchema, FormFieldSchema } from '@app/shared/forms';
import { DatePickerModule } from 'primeng/datepicker';

@Component({
  selector: 'app-date-input',
  imports: [ReactiveFormsModule, DatePickerModule],
  template: `
    <label>{{ field().label }}</label>
    <p-datepicker
      [formControl]="$any(form().get(field().key))"
      [disabled]="field().disabled"
      [showTime]="dateField().withTime"
    />
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
export class DateInput {
  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();
  dateField = computed(() => this.field() as DateFormFieldSchema<unknown>);
}
