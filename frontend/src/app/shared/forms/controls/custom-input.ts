import { Component, computed, input } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

import { NgComponentOutlet } from '@angular/common';
import { BaseIcon } from 'primeng/icons/baseicon';
import { CustomFormFieldSchema, FormFieldSchema } from '../form-schema.model';

@Component({
  selector: 'app-custom-input',
  imports: [ReactiveFormsModule, NgComponentOutlet, BaseIcon],
  template: `
    <ng-component
      [ngComponentOutlet]="custom().template"
      [ngComponentOutletInputs]="{ field: custom(), form: form() }"
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
export class CustomInput {
  field = input.required<FormFieldSchema<unknown>>();
  custom = computed(() => this.field() as CustomFormFieldSchema<unknown>);
  form = input.required<FormGroup>();
}
