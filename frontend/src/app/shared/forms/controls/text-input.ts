import { Component, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldSchema } from '@app/shared/forms';
import { InputText } from 'primeng/inputtext';

@Component({
  selector: 'app-text-input',
  imports: [ReactiveFormsModule, InputText],
  template: `
    <label>{{ field().label }}</label>
    <input
      pInputText
      [placeholder]="field().placeholder"
      [formControl]="$any(form().get(field().key))"
      [disabled]="field().disabled"
      [ariaDisabled]="field().disabled"
      [readOnly]="field().disabled"
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
export class TextInput {
  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();
}
