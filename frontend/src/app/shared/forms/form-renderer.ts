import { NgClass } from '@angular/common';
import {
  Component,
  effect,
  inject,
  Injector,
  input,
  runInInjectionContext,
} from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CheckboxInput } from './controls/checkbox-input';
import { CustomInput } from './controls/custom-input';
import { DateInput } from './controls/date-input';
import { SelectInput } from './controls/select-input';
import { TextInput } from './controls/text-input';
import { TextareaInput } from './controls/textarea-input';
import { FormSchema } from './form-schema.model';
import { FormRulesService } from './services/form-rule.service';

@Component({
  selector: 'app-form-renderer',
  imports: [
    TextInput,
    CheckboxInput,
    NgClass,
    ReactiveFormsModule,
    SelectInput,
    TextareaInput,
    CustomInput,
    DateInput,
  ],
  template: `
    <form [formGroup]="form()" class="form-grid" [ngClass]="schema().layout">
      @for (field of schema().fields; track field.key) {
        <div class="form-field">
          @if (field.type === 'text') {
            <app-text-input [field]="field" [form]="form()" />
          }

          @if (field.type === 'checkbox') {
            <app-checkbox-input [field]="field" [form]="form()" />
          }

          @if (field.type === 'select') {
            <app-select-input [field]="field" [form]="form()" />
          }

          @if (field.type === 'date') {
            <app-date-input [field]="field" [form]="form()" />
          }

          @if (field.type === 'textarea') {
            <app-textarea-input [field]="field" [form]="form()" />
          }

          @if (field.type === 'custom') {
            <app-custom-input [field]="field" [form]="form()" />
          }
        </div>
      }
    </form>
  `,
  styles: `
    :host form {
      display: grid;
      gap: 1rem;
    }
  `,
})
export class FormRenderer {
  readonly #ruleService = inject(FormRulesService);
  readonly #injector = inject(Injector);

  schema = input.required<FormSchema<unknown>>();
  form = input.required<FormGroup>();

  ngOnInit(): void {
    runInInjectionContext(this.#injector, () => {
      const formValue = toSignal(this.form().valueChanges, {
        initialValue: this.form().getRawValue(),
      });
      effect(() => {
        const value = formValue();
        this.#ruleService.applyRules(this.schema(), this.form(), value);
      });
    });
  }
}
