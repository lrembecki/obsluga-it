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
import { CollectionInput } from './controls/collection-input';
import { GroupInput } from './controls/group-input';
import { RenderInput } from './controls/render-input';
import { FormRulesService } from './services/form-rule.service';

@Component({
  selector: 'app-form-renderer',
  imports: [
    NgClass,
    ReactiveFormsModule,
    RenderInput,
    CollectionInput,
    GroupInput,
  ],
  template: `
    <form [formGroup]="form()" class="form-grid" [ngClass]="schema().layout">
      @for (field of schema().fields; track field.key) {
        @if (field.isVisible) {
          <div class="form-field">
            @if (field.type === 'collection') {
              <app-collection-input [field]="field" [form]="form()" />
            }
            @if (field.type === 'group') {
              <app-group-input [field]="field" [form]="form()" />
            } @else {
              <app-render-input [field]="field" [form]="form()" />
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
