import {
  computed,
  EnvironmentProviders,
  inject,
  Provider,
  Signal,
} from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute, LoadChildren, Route, Routes } from '@angular/router';
import { provideFeatureTemplate } from 'app/shared/templates/feature-template.service';
import { PermissionType } from '../defaults/permission.default';
import { authorizedGuard } from '../guards/authorized-guard';

export function getRoute(depth: number = 0) {
  let route = inject(ActivatedRoute);
  for (let i = 0; i < depth; i++) route = route.parent!;
  return route;
}

export function paramsSignal(depth: number = 0) {
  const route = getRoute(depth);
  const signal = toSignal(route.params);

  return computed(() => {
    return signal() ? signal()! : {};
  });
}

export function paramKeySignal(
  keys: string[],
  depth: number = 0,
): Signal<{ [key: string]: unknown }> {
  const params = paramsSignal(depth);
  return computed(() => {
    return keys.reduce((acc, cur) => ({ ...acc, [cur]: params()[cur] }), {});
  });
}

export function featureRoute(
  path: string,
  label: string,
  permissions: PermissionType[],
  loadChildren: LoadChildren,
): Route {
  return {
    path,
    data: {
      label,
      permissions,
    },
    loadChildren,
    canActivate: [authorizedGuard(permissions)],
  };
}

export function routeTemplate(params: {
  path?: string;
  providers: Array<Provider | EnvironmentProviders>;
  serviceProvider: () => Record<string, unknown>;
  children: Routes;
}): Route {
  return {
    path: params.path ?? '',
    providers: [
      params.providers,
      provideFeatureTemplate(params.serviceProvider),
    ],
    loadComponent: () =>
      import('app/shared/templates/feature-template').then(
        (e) => e.FeatureTemplate,
      ),
    children: params.children,
  };
}
