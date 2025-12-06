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
import { FormSchema } from '@app/shared/forms';
import { CheckboxInput } from './controls/checkbox-input';
import { CollectionInput } from './controls/collection-input';
import { CustomInput } from './controls/custom-input';
import { DateInput } from './controls/date-input';
import { FileUploadInput } from './controls/file-upload-input';
import { ImageInput } from './controls/image-input';
import { MultiSelectInput } from './controls/multi-select-input';
import { SelectInput } from './controls/select-input';
import { TextInput } from './controls/text-input';
import { TextareaInput } from './controls/textarea-input';
import { FormRulesService } from './services/form-rule.service';

@Component({
  selector: 'app-form-renderer',
  imports: [
    TextInput,
    CheckboxInput,
    NgClass,
    ReactiveFormsModule,
    SelectInput,
    MultiSelectInput,
    TextareaInput,
    CustomInput,
    DateInput,
    FileUploadInput,
    ImageInput,
    CollectionInput,
  ],
  template: `
    <form [formGroup]="form()" class="form-grid" [ngClass]="schema().layout">
      @for (field of schema().fields; track field.key) {
        @if (field.isVisible) {
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

            @if (field.type === 'multiselect') {
              <app-multi-select-input [field]="field" [form]="form()" />
            }

            @if (field.type === 'date') {
              <app-date-input [field]="field" [form]="form()" />
            }

            @if (field.type === 'textarea') {
              <app-textarea-input [field]="field" [form]="form()" />
            }

            @if (field.type === 'image') {
              <app-image-input [field]="field" [form]="form()" />
            }

            @if (field.type === 'fileupload') {
              <app-file-upload-input [field]="field" [form]="form()" />
            }

            @if (field.type === 'custom') {
              <app-custom-input [field]="field" [form]="form()" />
            }

            @if (field.type === 'collection') {
              <app-collection-input [field]="field" [form]="form()" />
            }
          </div>
        }
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
