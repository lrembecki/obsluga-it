import { Component, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CheckboxModule } from 'primeng/checkbox';
import { FormFieldSchema } from '@app/shared/forms';

@Component({
  selector: 'app-checkbox-input',
  imports: [ReactiveFormsModule, CheckboxModule],
  template: `
    <label>{{ field().label }}</label>
    <p-checkbox binary="true" [formControl]="$any(form().get(field().key))" />
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
export class CheckboxInput {
  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();
}
