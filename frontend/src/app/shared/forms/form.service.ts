import { EnvironmentProviders, inject, Provider, signal } from '@angular/core';
import { ApiFacade } from '@app/core/interfaces/facade.interface';
import { FormSchema } from './form-schema.model';

export function provideFormService(
  provider: any,
): (Provider | EnvironmentProviders)[] {
  return [provider, { provide: FormService, useExisting: provider }];
}

export abstract class FormService<T> {
  protected readonly _schema = signal<FormSchema<T>>(null!);
  protected readonly _returnRoute = signal<string[]>(['..']);

  public readonly schema = this._schema.asReadonly();
  public readonly returnRoute = this._returnRoute.asReadonly();
  public readonly facade = inject(ApiFacade);

  initialize(): Promise<void> {
    return this.facade.initialize();
  }
}
