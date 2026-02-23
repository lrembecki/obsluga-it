/* eslint-disable no-unused-vars */
import {
  computed,
  EnvironmentProviders,
  inject,
  InjectionToken,
  Provider,
  signal,
  Signal,
  WritableSignal,
} from '@angular/core';
import { Subject } from 'rxjs';
import { ServiceCallResult } from '../models/service-call-result.model';
import { ApiService } from '../services/api.service';

export interface IDataFacade<T> {
  data: Signal<T>;
}
export interface IArrayFacade<T> extends IDataFacade<T[]> {}
export interface IPopulateFacade {
  loading: Signal<boolean>;
  initialized: Signal<boolean>;
  initialize(): Promise<void>;
  populate(): Promise<this>;
}
export interface IEditableFacade {
  saving: Signal<boolean>;
  deleting: Signal<boolean>;

  update(endpoint: string, model: unknown): Promise<ServiceCallResult<unknown>>;
  create(endpoint: string, model: unknown): Promise<ServiceCallResult<unknown>>;
  delete(endpoint: string): Promise<ServiceCallResult<unknown>>;
}

export interface Facade extends IPopulateFacade, IEditableFacade {}
export interface ArrayFacade<T> extends IArrayFacade<T>, Facade {}
export interface ObjectFacade<T> extends IDataFacade<T>, Facade {}

export function provideArrayApiFacade(
  provider: any,
): (Provider | EnvironmentProviders)[] {
  return [
    { provide: ArrayApiFacade, useExisting: provider },
    { provide: populateFacadeScope, useExisting: provider },
    { provide: facadeScope, useExisting: provider },
  ];
}

export const populateFacadeScope = new InjectionToken<IPopulateFacade>(
  'populateFacadeScope',
);
export const facadeScope = new InjectionToken<Facade>('facadeScope');

export function provideObjectApiFacade(
  provider: any,
): (Provider | EnvironmentProviders)[] {
  return [
    { provide: ObjectApiFacade, useExisting: provider },
    { provide: populateFacadeScope, useExisting: provider },
    { provide: facadeScope, useExisting: provider },
  ];
}

export abstract class ObjectApiFacade<T>
  implements ObjectFacade<T>, IPopulateFacade
{
  protected readonly _data: WritableSignal<T> = null!;
  protected readonly _initialized = signal(false);
  protected readonly _requiresInitialization = signal(true);
  protected readonly _api = inject(ApiService);
  protected readonly _loading = signal(false);
  protected readonly _saving = signal(false);
  protected readonly _deleting = signal(false);
  protected readonly _filter = signal<Record<string, unknown>>({});
  protected readonly _endpoint: string = null!;

  public readonly data = computed(() => this.withData(this._data()));
  public readonly initialized = this._initialized.asReadonly();
  public readonly loading = this._loading.asReadonly();
  public readonly saving = this._saving.asReadonly();
  public readonly deleting = this._deleting.asReadonly();

  private readonly _onLoaded = new Subject();
  public get onLoaded() {
    return this._onLoaded.asObservable();
  }

  constructor(defaultData: T, endpoint: string) {
    this._endpoint = endpoint;
    this._data = signal<T>(defaultData);
  }

  async initialize(): Promise<void> {
    if (this._requiresInitialization() && this._initialized()) return;

    await this.populate();
  }

  protected async populateByRoute<Y>(
    route: string,
  ): Promise<ServiceCallResult<Y>> {
    const headers: Record<string, string> = {};
    const filter = this._filter();

    if (Object.values(filter).length) {
      headers['filter'] = JSON.stringify(filter);
    }

    this._loading.set(true);

    let endpoint = `${this._endpoint}`;
    if (route) {
      endpoint += `/${route}`;
    }

    const response = await this._api.get<Y>(endpoint, {}, headers);

    this._loading.set(false);

    return response;
  }

  async populate(): Promise<this> {
    if (this._loading()) {
      await Promise.resolve(() => {
        while (this._loading());
      });
      return this;
    }

    const response = await this.populateByRoute<T>(null!);
    this._loading.set(false);

    if (response.success && response.data) {
      this._data.set(response.data);
      this._initialized.set(true);
    }

    this._onLoaded.next(null!);

    return this;
  }

  async update(
    endpoint: string,
    model: unknown,
  ): Promise<ServiceCallResult<T>> {
    this._saving.set(true);

    const response = await this._api.put<T>(
      `${this._endpoint}/${endpoint}`,
      model,
    );

    this._saving.set(false);

    if (response.success) await this.populate();

    return response;
  }

  async create(
    endpoint: string,
    model: unknown,
  ): Promise<ServiceCallResult<T>> {
    endpoint ??= '';

    this._saving.set(true);

    const response = await this._api.post<T>(
      `${this._endpoint}${endpoint}`,
      model,
    );

    this._saving.set(false);

    if (response.success) await this.populate();

    return response;
  }

  async delete(endpoint: string): Promise<ServiceCallResult<T>> {
    this._deleting.set(true);
    const response = await this._api.delete<T>(`${this._endpoint}/${endpoint}`);
    this._deleting.set(false);

    if (response.success) await this.populate();

    return response;
  }

  protected withData(data: T) {
    return data;
  }

  public async sort(sort: {
    column: string;
    direction: 'asc' | 'desc';
  }): Promise<void> {
    await this.filter({
      ...this._filter(),
      sort,
    });
  }

  public async filter(filter: Record<string, unknown>): Promise<void> {
    const a = JSON.stringify(filter);
    const b = JSON.stringify(this._filter());
    if (a !== b) {
      this._filter.set(filter);
      await this.populate();
    }
  }
}

export abstract class ArrayApiFacade<T> extends ObjectApiFacade<T[]> {
  public getId(model: T): string {
    return (model as any).id;
  }

  public getById(id: string): T | null {
    return this.data().find((e) => this.getId(e) === id) ?? null;
  }

  public async populateById(id: string): Promise<this> {
    const response = await this.populateByRoute<unknown>(id);

    if (response.success && response.data) {
      const data = this._data();
      const existing = data.findIndex((e) => this.getId(e) === id);
      data[existing] = response.data as T;
      this._data.set([...data]);
    }

    return this;
  }
}
