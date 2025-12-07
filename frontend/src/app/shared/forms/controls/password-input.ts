import { Component, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldSchema } from '@app/shared/forms';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-password-input',
  imports: [ReactiveFormsModule, InputTextModule],
  template: `
    <label>{{ field().label }}</label>
    <input
      type="password"
      pInputText
      [placeholder]="field().placeholder"
      [formControl]="$any(form().get(field().key))"
      autocomplete="new-password"
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
export class PasswordInput {
  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();
}
