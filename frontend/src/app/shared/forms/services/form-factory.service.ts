import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormSchema } from '../form-schema.model';

@Injectable({ providedIn: 'root' })
export class FormFactoryService {
  createForm<T>(
    schema: FormSchema<T>,
    mode: 'create' | 'edit' = 'create',
  ): FormGroup {
    const controls: any = {};

    schema.fields.forEach((field) => {
      const isDisabled =
        field.disabled ||
        (field.disabledOnEdit && mode === 'edit') ||
        (field.disabledOnCreate && mode === 'create');

      controls[field.key] = field.createControl(mode, !!isDisabled);
    });

    return new FormGroup(controls);
  }
}
