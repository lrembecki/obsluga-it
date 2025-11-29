import { Directive, inject, Signal, signal } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormSchema } from './form-schema.model';
import { FormFactoryService } from './services/form-factory.service';

@Directive()
export abstract class BaseFormComponent<T> {
  form = signal<FormGroup | null>(null);
  abstract schema: Signal<FormSchema<T>>;
  formFactory = inject(FormFactoryService);

  // eslint-disable-next-line no-unused-vars
  abstract submit(data: T): Promise<void>;

  async onSubmit() {
    if (this.form()!.invalid) {
      this.form()!.markAllAsTouched();
      return;
    }
    await this.submit(this.form()!.value);
  }
}
