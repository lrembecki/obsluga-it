import {
  computed,
  effect,
  EnvironmentProviders,
  inject,
  Provider,
  signal,
} from '@angular/core';
import { ApiFacade } from '@app/core/interfaces/facade.interface';
import { FormSchema } from '../forms';
import { DataTableColumnSchema, DataTableSchema } from './data-table.types';

export function provideDataTableService(
  provider: any,
): (Provider | EnvironmentProviders)[] {
  return [provider, { provide: DataTableService, useExisting: provider }];
}

export abstract class DataTableService<T> {
  public readonly facade = inject(ApiFacade<T>);
  protected readonly _data = signal<T[]>([]);
  protected readonly _schema = signal<DataTableSchema<T>>(null!);
  protected readonly _filterSchema = signal<FormSchema<any>>(null!);

  public readonly canCreate = signal(true);
  public readonly columns = computed(() =>
    this.fetchColumnSchema(this.schema()?.columns ?? []),
  );
  public readonly persistenceKey = computed(
    () => this.schema()?.persistenceKey,
  );
  public readonly data = computed(() => this.fetchData(this._data()));
  public readonly schema = computed(() => this.fetchSchema(this._schema()));

  constructor() {
    effect(() => this._data.set(this.facade.data()));
  }

  fetchData(data: T[]): T[] {
    return data;
  }
  fetchSchema(schema: DataTableSchema<T>): DataTableSchema<T> {
    return schema;
  }

  fetchColumnSchema(
    columns: DataTableColumnSchema<T>[],
  ): DataTableColumnSchema<T>[] {
    return columns;
  }

  initialize(): Promise<void> {
    return Promise.resolve();
  }
}
