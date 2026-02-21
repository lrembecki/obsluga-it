import { Component, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormSchema, FormFieldSchema } from './form-field.schema';

@Component({
  selector: 'app-form-renderer',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    @if (form() && schema()) {
      <form [formGroup]="form()!">
        @for (field of schema()!.fields; track field.key) {
          <div class="form-field">
            <label [for]="field.key">{{ field.label }}</label>
            @switch (field.type) {
              @case ('text') {
                <input [id]="field.key" type="text" [formControlName]="field.key" />
              }
              @case ('textarea') {
                <textarea [id]="field.key" [formControlName]="field.key"></textarea>
              }
              @case ('number') {
                <input [id]="field.key" type="number" [formControlName]="field.key" />
              }
              @case ('date') {
                <input [id]="field.key" type="date" [formControlName]="field.key" />
              }
              @case ('checkbox') {
                <input [id]="field.key" type="checkbox" [formControlName]="field.key" />
              }
              @case ('password') {
                <input [id]="field.key" type="password" [formControlName]="field.key" />
              }
              @default {
                <input [id]="field.key" type="text" [formControlName]="field.key" />
              }
            }
          </div>
        }
      </form>
    }
  `,
  styles: [`
    .form-field {
      display: flex;
      flex-direction: column;
      margin-bottom: 1rem;
    }
    label {
      font-weight: 600;
      margin-bottom: 0.25rem;
    }
    input, textarea {
      padding: 0.5rem;
      border: 1px solid var(--border);
      border-radius: 4px;
    }
  `],
})
export class FormRenderer {
  schema = input<FormSchema<any> | null>(null);
  form = input<FormGroup | null>(null);
}
