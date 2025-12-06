import { CommonModule } from '@angular/common';
import { Component, computed, input } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormFieldSchema, TextareaFormFieldSchema } from '@app/shared/forms';
import { TextareaModule } from 'primeng/textarea';

@Component({
  selector: 'app-textarea-input',
  imports: [ReactiveFormsModule, TextareaModule, CommonModule, FormsModule],
  template: `
    <label>{{ textareaField().label }}</label>
    <textarea
      [rows]="textareaField().rows"
      [cols]="textareaField().cols"
      class="textarea-input"
      pTextarea
      [formControl]="$any(form().get(textareaField().key))"
      style="margin-bottom: .5rem;"
    ></textarea>
  `,
  host: {
    class: 'input-container',
  },
  styles: `
    :host,
    .textarea-input {
      display: flex;
      flex-direction: column;
      gap: 0.25rem;
    }
  `,
})
export class TextareaInput {
  field = input.required<FormFieldSchema<unknown>>();
  textareaField = computed(
    () => this.field() as TextareaFormFieldSchema<unknown>,
  );
  form = input.required<FormGroup>();
}
