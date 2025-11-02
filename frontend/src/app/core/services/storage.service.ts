import { Injectable, Signal, signal, WritableSignal } from '@angular/core';
import { AccountModel } from '../models/account.model';

export interface IStorage<T> {
  readonly key: string;
  readonly data: Signal<T | null>;

  initialize(): T | null;
  set(value: T): void;
}

@Injectable({ providedIn: 'root' })
export class StorageService {
  public readonly b2cToken = new StringLocalStorage('b2c-token');
  public readonly account = new ModelLocalStorage<AccountModel>(
    'account-data',
    (raw) => new AccountModel(JSON.parse(raw)),
  );
  public readonly language = new StringLocalStorage('language');
}

class ModelLocalStorage<T> implements IStorage<T> {
  protected readonly _signal: WritableSignal<T | null> = null!;
  public readonly data: Signal<T | null> = null!;
  constructor(
    public readonly key: string,
    private readonly parse: (raw: string) => T,
  ) {
    this._signal = signal<T | null>(this.initialize());
    this.data = this._signal.asReadonly();
  }

  public initialize(): T | null {
    const raw = localStorage.getItem(this.key);
    return raw?.length ? this.parse(raw) : null!;
  }

  set(value: T): void {
    if (value) {
      localStorage.setItem(this.key, JSON.stringify(value));
    } else {
      localStorage.removeItem(this.key);
    }
    this._signal.set(value);
  }
}

class StringLocalStorage implements IStorage<string> {
  constructor(public readonly key: string) {
    this._signal.set(this.initialize());
  }

  protected readonly _signal = signal<string | null>(this.initialize());
  public readonly data = this._signal.asReadonly();

  public initialize(): string | null {
    const raw = localStorage.getItem(this.key);
    return raw?.length ? raw : null!;
  }

  set(value: string): void {
    if (value) {
      localStorage.setItem(this.key, value);
    } else {
      localStorage.removeItem(this.key);
    }

    this._signal.set(value);
  }
}
