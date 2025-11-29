import { Component, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DatePickerModule } from 'primeng/datepicker';
import { FormFieldSchema } from '../form-schema.model';

@Component({
  selector: 'app-date-input',
  imports: [ReactiveFormsModule, DatePickerModule],
  template: `
    <label>{{ field().label }}</label>
    <p-datepicker [formControl]="$any(form().get(field().key))" />
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
}
