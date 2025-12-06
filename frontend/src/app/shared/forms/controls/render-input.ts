import { Component, input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormFieldSchema } from '../form-field.schema';
import { CheckboxInput } from './checkbox-input';
import { CustomInput } from './custom-input';
import { DateInput } from './date-input';
import { FileUploadInput } from './file-upload-input';
import { ImageInput } from './image-input';
import { MultiSelectInput } from './multi-select-input';
import { SelectInput } from './select-input';
import { TextInput } from './text-input';
import { TextareaInput } from './textarea-input';

@Component({
  selector: 'app-render-input',
  imports: [
    TextInput,
    CheckboxInput,
    SelectInput,
    MultiSelectInput,
    DateInput,
    TextareaInput,
    ImageInput,
    FileUploadInput,
    CustomInput,
  ],
  template: `
    @if (field().type === 'text') {
      <app-text-input [field]="field()" [form]="form()" />
    }

    @if (field().type === 'checkbox') {
      <app-checkbox-input [field]="field()" [form]="form()" />
    }

    @if (field().type === 'select') {
      <app-select-input [field]="field()" [form]="form()" />
    }

    @if (field().type === 'multiselect') {
      <app-multi-select-input [field]="field()" [form]="form()" />
    }

    @if (field().type === 'date') {
      <app-date-input [field]="field()" [form]="form()" />
    }

    @if (field().type === 'textarea') {
      <app-textarea-input [field]="field()" [form]="form()" />
    }

    @if (field().type === 'image') {
      <app-image-input [field]="field()" [form]="form()" />
    }

    @if (field().type === 'fileupload') {
      <app-file-upload-input [field]="field()" [form]="form()" />
    }

    @if (field().type === 'custom') {
      <app-custom-input [field]="field()" [form]="form()" />
    }
  `,
  styles: ``,
})
export class RenderInput {
  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();
}
