/* eslint-disable no-unused-vars */
import { computed, signal } from '@angular/core';
import { Facade } from '../interfaces/facade.interface';

export class CacheService<T> {
  private readonly _data = signal<CacheItem<T>[]>([]);
  private readonly facade: Facade<T>;
  private readonly identityCallback: (record: T) => string;

  constructor(
    facade: Facade<T>,
    identityCallback: (record: T) => string,
  ) {
    this.facade = facade;
    this.identityCallback = identityCallback;
  }

  public readonly data = computed(() => {
    const facadeData = this.facade.data().map(
      (e) =>
        new CacheItem<T>({
          isDeleted: false,
          identity: this.identityCallback(e),
          item: JSON.parse(JSON.stringify(e)),
        }),
    );
    const data = this._data();

    return this._data()
      .concat(
        ...facadeData.filter(
          (e) => !data.map((y) => y.identity).includes(e.identity),
        ),
      )
      .filter((e) => e.isDeleted === false);
  });

  public readonly pendingChanges = this._data.asReadonly();

  public delete(item: T): void {
    const cachedItem = new CacheItem({
      identity: this.identityCallback(item),
      item,
      isDeleted: true,
    });

    this._data.set([
      cachedItem,
      ...this._data().filter((e) => e.identity !== cachedItem.identity),
    ]);
  }

  public discard(item?: CacheItem<T>): void {
    if (item) {
      this._data.set(this._data().filter((e) => e !== item));
    } else {
      this._data.set([]);
    }
  }

  public update(cacheItem: CacheItem<T>): void {
    const cachedItem = new CacheItem({
      identity: this.identityCallback(cacheItem.item),
      item: JSON.parse(JSON.stringify(cacheItem.item)),
    });

    this._data.set([
      cachedItem,
      ...this._data().filter((e) => e.identity !== cachedItem.identity),
    ]);
  }
}

export class CacheItem<T> {
  isDeleted: boolean = false;
  identity: string = null!;
  readonly item: T = null!;

  constructor(init?: Partial<CacheItem<T>>) {
    Object.assign(this, init);
  }
}
