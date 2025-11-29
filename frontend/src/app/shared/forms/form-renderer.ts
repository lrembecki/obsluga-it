import { NgClass } from '@angular/common';
import { Component, effect, inject, input } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CheckboxInput } from './controls/checkbox-input';
import { SelectInput } from "./controls/select-input";
import { TextInput } from "./controls/text-input";
import { TextareaInput } from "./controls/textarea-input";
import { Error } from "./error";
import { FormSchema } from './form-schema.model';
import { FormRulesService } from './services/form-rule.service';

@Component({
  selector: 'app-form-renderer',
  imports: [
    TextInput,
    CheckboxInput,
    Error,
    NgClass,
    ReactiveFormsModule,
    SelectInput,
    TextareaInput,
  ],
  template: `
    <form [formGroup]="form()" class="form-grid" [ngClass]="schema().layout">

      @for (field of schema().fields; track field.key) {
        <div class="form-field">

          @if (field.type === 'text') {
            <app-text-input
              [field]="field"
              [form]="form()" />
          }

          @if (field.type === 'checkbox') {
            <app-checkbox-input
              [field]="field"
              [form]="form()" />
          }

          @if (field.type === 'select') {
            <app-select-input
              [field]="field"
              [form]="form()" />
          }

          @if (field.type === 'textarea') {
            <app-textarea-input
              [field]="field"
              [form]="form()" />
          }

          <app-error [control]="form().get(field.key)!"></app-error>
        </div>
      }
    </form>
  `,
  styles: `
    :host form {
      display: grid;
      gap: 1rem;
    }
  `
})
export class FormRenderer {
  readonly #ruleService = inject(FormRulesService);

  schema = input.required<FormSchema<unknown>>();
  form = input.required<FormGroup>();

  ngOnInit(): void {
    const formValue = toSignal(this.form().valueChanges, {
      initialValue: this.form().getRawValue(),
    });
    effect(() => {
      const value = formValue();
      this.#ruleService.applyRules(this.schema(), this.form(), value);
    });
  }
}
