import {
  computed,
  EnvironmentProviders,
  inject,
  Provider,
  signal,
} from '@angular/core';
import { ApiFacade } from '@app/core/interfaces/facade.interface';
import { FormSchema } from './form-schema.model';
import { FormFactoryService } from './services/form-factory.service';

export function provideFormService(
  provider: any,
): (Provider | EnvironmentProviders)[] {
  return [provider, { provide: FormService, useExisting: provider }];
}

export abstract class FormService<T> {
  protected readonly _schema = signal<FormSchema<T>>(null!);
  protected readonly _returnRoute = signal<string[]>(['../list']);
  protected readonly _formFactory = inject(FormFactoryService);

  public readonly schema = this._schema.asReadonly();
  public readonly returnRoute = this._returnRoute.asReadonly();
  public readonly facade = inject(ApiFacade);
  public readonly id = signal<string>('create');
  public readonly model = computed(() =>
    this.facade.data().find((e) => e.id === this.id()),
  );
  public readonly isLoading = this.facade.loading;
  public readonly mode = computed(() =>
    this.id() === 'create' ? 'create' : 'edit',
  );
  public readonly form = computed(() => {
    if (this.mode() === 'create') {
      return this._formFactory.createForm(this.schema());
    } else {
      const formGroup = this._formFactory.createForm(
        this.schema(),
        this.mode(),
      );
      const model = this.model();
      // Populate FormArray controls for collection fields before patching values
      this.schema().fields.forEach((field: any) => {
        if (field.type === 'collection') {
          const value = model?.[field.key as string] ?? ([] as any[]);
          const fa = formGroup.get(field.key as string) as any;
          if (fa && Array.isArray(value)) {
            // Clear existing
            while (fa.length > 0) fa.removeAt(0);
            // Push item controls
            value.forEach((item: any) => {
              if (field.itemFields?.length) {
                const groupControls: Record<string, any> = {};
                field.itemFields.forEach((f: any) => {
                  const ctrl = f.createControl('edit', !!f.disabled);
                  groupControls[f.key as string] = ctrl;
                });
                const fg = new (formGroup.constructor as any)(groupControls);
                fa.push(fg);
              } else {
                fa.push(
                  new (
                    formGroup.constructor as any
                  ).prototype.constructor.control(item),
                );
              }
            });
          }
        }
      });

      formGroup.patchValue(model);

      return formGroup;
    }
  });

  initialize(): Promise<void> {
    return this.facade.initialize();
  }

  async submit(data: any): Promise<boolean> {
    const model = structuredClone(this.model()) ?? data;
    Object.assign(model, data);

    const response =
      this.id() === 'create'
        ? await this.facade.create('', model)
        : await this.facade.update(this.id(), model);

    return response.success;
  }
}
