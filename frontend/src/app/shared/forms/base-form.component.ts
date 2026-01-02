import { Directive, inject, linkedSignal, Signal } from '@angular/core';
import { FormSchema } from '@app/shared/forms';
import { FormService } from './form.service';
import { createFormFromFieldSchema } from './services/form-factory.service';

@Directive()
export abstract class BaseFormComponent<T> {
  readonly _service = inject(FormService<T>);
  abstract schema: Signal<FormSchema<T>>;

  // eslint-disable-next-line no-unused-vars
  abstract submit(data: T): Promise<void>;

  public readonly form = linkedSignal(() => {
    this._service.cleanupOnChangeSubscriptions();

    const mode = this._service.mode();
    const schema = this._service.schema();
    const model = this._service.model();
    const defaultValue = mode === 'create' ? schema.patchValue : model;
    const formGroup = createFormFromFieldSchema(schema, mode);
    const modelResult = this._service.buildFormControls(
      formGroup,
      defaultValue,
    );

    formGroup.patchValue(modelResult);

    this._service.registerOnChangeHandlers(schema, formGroup);

    return formGroup;
  });

  async onSubmit() {
    if (this.form().invalid) {
      this.form().markAllAsTouched();

      return;
    }
    await this.submit(this.form().value);
  }
}
