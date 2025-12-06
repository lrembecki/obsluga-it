import { Component, computed, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { AccordionModule } from 'primeng/accordion';
import { FormFieldSchema } from '../form-field.schema';
import { GroupFormFieldSchema } from '../schemas/group-form.schema';
import { CollectionInput } from './collection-input';
import { RenderInput } from './render-input';

@Component({
  selector: 'app-group-input',
  imports: [AccordionModule, ReactiveFormsModule, CollectionInput, RenderInput],
  template: `
    <p-accordion value="0">
      <p-accordion-panel value="0">
        <p-accordion-header>{{ field().label }}</p-accordion-header>
        <p-accordion-content>
          <form [formGroup]="groupControl()" class="form-grid">
            @for (field of formField().nestedFields; track field.key) {
              @if (field.isVisible) {
                <div class="form-field">
                  @if (field.type === 'collection') {
                    <app-collection-input [field]="field" [form]="form()" />
                  } @else {
                    <app-render-input [field]="field" [form]="form()" />
                  }
                </div>
              }
            }
          </form>
        </p-accordion-content>
      </p-accordion-panel>
    </p-accordion>
  `,
  styles: ``,
})
export class GroupInput {
  field = input.required<FormFieldSchema<unknown>>();
  formField = computed(
    () => this.field() as any as GroupFormFieldSchema<unknown, unknown>,
  );
  form = input.required<FormGroup>();

  groupControl = computed(
    () => this.form().get(this.field().key) as any as FormGroup,
  );
}
