import { computed, Signal } from '@angular/core';
import { Facade } from '../interfaces/facade.interface';

export declare type FacadesObject = {
  [key: string]: Facade<unknown>;
};

export function populateFacades(provide: Record<string, unknown>) {
  return filterFacades(provide).map((e) => e.populate());
}

export function isLoadingComputed(provide: Record<string, unknown>) {
  return computed(() => isLoading(provide));
}

export function filterFacades(provide: Record<string, unknown>) {
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

export function isLoading(provide: Record<string, unknown>): boolean {
  return filterFacades(provide).some((e) => e.loading());
}
