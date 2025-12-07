import {
  computed,
  EnvironmentProviders,
  inject,
  Provider,
  signal,
  Signal,
  WritableSignal,
} from '@angular/core';
import { Subject } from 'rxjs';
import { ServiceCallResult } from '../models/service-call-result.model';
import { ApiService } from '../services/api.service';

export interface Facade<T> {
  data: Signal<T[]>;
  loading: Signal<boolean>;
  initialized: Signal<boolean>;

  initialize(): Promise<void>;
  populate(): Promise<this>;
}

export function provideApiFacade(
  provider: any,
): (Provider | EnvironmentProviders)[] {
  return [provider, { provide: ApiFacade, useExisting: provider }];
}

export abstract class ApiFacade<T> implements Facade<T> {
  protected readonly _data: WritableSignal<T[]> = null!;
  protected readonly _initialized = signal(false);
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

  constructor(defaultData: T[], endpoint: string) {
    this._endpoint = endpoint;
    this._data = signal<T[]>(defaultData);
  }

  getById(id: string): T | null {
    return this.data().find((e) => (e as any).id === id) ?? null;
  }

  async initialize(): Promise<void> {
    if (this._initialized()) return;

    await this.populate();
  }

  async populate(): Promise<this> {
    if (this._loading()) {
      await Promise.resolve(() => {
        while (this._loading());
      });
      return this;
    }

    const headers: Record<string, string> = {};

    if (Object.values(this._filter()).length) {
      headers['filter'] = JSON.stringify(this._filter());
    }

    this._loading.set(true);
    const response = await this._api.get<T[]>(this._endpoint, {}, headers);
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

  protected withData(data: T[]) {
    return data;
  }

  public async filter(filter: Record<string, unknown>): Promise<void> {
    const model = structuredClone(filter);

    if (JSON.stringify(model) !== JSON.stringify(this._filter)) {
      this._filter.set(model);
      await this.populate();
    }
  }
}
