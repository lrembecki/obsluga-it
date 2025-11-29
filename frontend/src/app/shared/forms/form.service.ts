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
  protected readonly _returnRoute = signal<string[]>(['..']);
  protected readonly _formFactory = inject(FormFactoryService);

  public readonly schema = this._schema.asReadonly();
  public readonly returnRoute = this._returnRoute.asReadonly();
  public readonly facade = inject(ApiFacade);
  public readonly id = signal<string>('create');
  public readonly model = computed(() =>
    this.facade.data().find((e) => e.id === this.id()),
  );
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

      formGroup.patchValue(this.model());

      return formGroup;
    }
  });

  initialize(): Promise<void> {
    return this.facade.initialize();
  }

  async submit(data: any): Promise<boolean> {
    const model = structuredClone(this.model()) ?? data;
    Object.assign(model, data);

    const response = this.id()
      ? await this.facade.update(this.id(), model)
      : await this.facade.create('', model);

    return response.success;
  }
}
