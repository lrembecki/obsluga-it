import { Directive, inject, signal } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ArrayFormService } from './form.service';

@Directive()
export abstract class BaseFormComponent<T> {
  protected readonly _service = inject(ArrayFormService<T>);
  protected readonly form = signal<FormGroup | null>(null);

  ngOnInit() {
    const schema = this._service.schema();
    if (schema) {
      const formGroup = schema.buildFormGroup();
      this.form.set(formGroup);
    }
  }

  protected async onSubmit(): Promise<void> {
    const formGroup = this.form();
    if (!formGroup || formGroup.invalid) return;
    await this.submit(formGroup.value);
  }

  protected abstract submit(data: T): Promise<void>;

  ngOnDestroy() {
    this._service.cleanupOnChangeSubscriptions();
  }
}
