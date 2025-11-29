import { Directive, inject, Signal, signal } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormSchema } from './form-schema.model';
import { FormFactoryService } from './services/form-factory.service';

@Directive()
export abstract class BaseFormComponent<T> {
  form = signal<FormGroup | null>(null);
  abstract schema: Signal<FormSchema<T>>;
  mode: 'create' | 'edit' = 'create';
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

  patch(data: Partial<T>) {
    if (!this.form()) return;

    this.form()!.patchValue(data);

    this.setEditMode();

    if ((this as any).onAfterPatch) {
      (this as any).onAfterPatch(data);
    }
  }

  setEditMode() {
    this.mode = 'edit';
  }

  setCreateMode() {
    this.mode = 'create';
  }
}
