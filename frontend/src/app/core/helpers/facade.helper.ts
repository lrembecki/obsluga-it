import { computed, Signal } from '@angular/core';
import { Facade } from '../interfaces/facade.interface';

export declare type FacadesObject = {
  [key: string]: Facade<unknown>;
};

export function populateFacades<T>(provide: T) {
  return filterFacades<T>(provide).map((e) => e.populate());
}

export function isLoadingComputed<T>(provide: T) {
  return computed(() => isLoading(provide));
}

export function filterFacades<T>(provide: T) {
  return Object.values(provide || {})
    .filter((e: any) => 'populate' in e)
    .map((e) => e as Facade<unknown>);
}

export function provideFacadeData<T>(
  key: string,
  facades: Record<string, unknown>,
): Signal<T> {
  return (facades[key] as Facade<T>).data;
}

export function isLoading<T>(provide: T): boolean {
  return filterFacades(provide).some((e) => e.loading());
}
