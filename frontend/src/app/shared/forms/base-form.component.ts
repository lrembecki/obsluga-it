import { Directive, inject, Signal } from '@angular/core';
import { FormSchema } from '@app/shared/forms';
import { FormService } from './form.service';
import { FormFactoryService } from './services/form-factory.service';

@Directive()
export abstract class BaseFormComponent<T> {
  readonly _service = inject(FormService<T>);
  abstract schema: Signal<FormSchema<T>>;
  formFactory = inject(FormFactoryService);

  // eslint-disable-next-line no-unused-vars
  abstract submit(data: T): Promise<void>;

  async onSubmit() {
    if (this._service.form().invalid) {
      this._service.form().markAllAsTouched();

      return;
    }
    await this.submit(this._service.form().value);
  }
}
