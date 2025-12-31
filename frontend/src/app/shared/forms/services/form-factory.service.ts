import { FormGroup } from '@angular/forms';
import { FormFieldSchema } from '@app/shared/forms';

export function createFormFromFieldSchema<T>(
  schema: { fields: FormFieldSchema<T>[] },
  mode: 'create' | 'edit' = 'create',
): FormGroup {
  const controls: any = {};

  (schema?.fields || []).forEach((field) => {
    const isDisabled =
      field.disabled ||
      (field.disabledOnEdit && mode === 'edit') ||
      (field.disabledOnCreate && mode === 'create');

    controls[field.key] = field.createControl(mode, !!isDisabled);
  });

  return new FormGroup(controls);
}
