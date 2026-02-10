import { inject, signal } from '@angular/core';
import { ApiService } from '../services/api.service';
import { FormDefinitionVM } from '../models/form-definition.vm';
import { lastValueFrom } from 'rxjs';

export class FormDefinitionsFacade {
  private readonly _api = inject(ApiService);
  private readonly _data = signal<FormDefinitionVM[]>([]);

  public readonly data = this._data.asReadonly();

  public async initialize(): Promise<void> {
    const formDefinitions = await lastValueFrom(
      this._api.get<FormDefinitionVM[]>('form-definitions/index'),
    );
    this._data.set(formDefinitions);
  }

  public getByKey(key: string): FormDefinitionVM {
    return this._data().find((def) => def.name === key)!;
  }
}
