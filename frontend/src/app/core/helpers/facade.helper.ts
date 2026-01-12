import { computed, Signal } from '@angular/core';
import { ArrayFacade, IPopulateFacade } from '../interfaces/facade.interface';

export declare type FacadesObject = {
  [key: string]: ArrayFacade<unknown>;
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
    .map((e) => e as IPopulateFacade);
}

export function provideFacadeData<T>(
  key: string,
  facades: Record<string, unknown>,
): Signal<T[]> {
  return (facades[key] as ArrayFacade<T>).data;
}

export function isLoading<T>(provide: T): boolean {
  return filterFacades(provide).some((e) => e.loading());
}
