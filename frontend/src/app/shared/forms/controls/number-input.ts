import { Component, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldSchema } from '@app/shared/forms';
import { InputNumberModule } from 'primeng/inputnumber';

@Component({
  selector: 'app-number-input',
  imports: [ReactiveFormsModule, InputNumberModule],
  template: `
    <label>{{ field().label }}</label>
    <p-inputNumber
      [placeholder]="field().placeholder"
      [formControl]="$any(form().get(field().key))"
    ></p-inputNumber>
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
export class NumberInput {
  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();
}
