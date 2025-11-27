import {
  computed,
  EnvironmentProviders,
  inject,
  Provider,
  signal,
  Signal,
  Type,
} from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import {
  ActivatedRoute,
  DefaultExport,
  LoadChildren,
  Route,
  Routes,
} from '@angular/router';
import { provideFeatureTemplate } from '@shared/templates/feature-template.service';
import { Observable } from 'rxjs';
import { PermissionType } from '../defaults/permission.default';
import { authorizedGuard } from '../guards/authorized-guard';


export function listRoute<VM, TProvider>(
  providers: (Provider | EnvironmentProviders)[],
  serviceProvider: () => TProvider,
  loadListComponent: () =>
    | Type<unknown>
    | Observable<Type<unknown> | DefaultExport<Type<unknown>>>
    | Promise<Type<unknown> | DefaultExport<Type<unknown>>>
): Route {
  return routeTemplate({
    providers: [providers],
    serviceProvider,
    children: [
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
      },
      {
        path: 'list',
        loadComponent: loadListComponent
      },
      {
        path: 'create',
        loadComponent: () => import('@shared/forms/form-template').then(c => c.FormTemplate),
        resolve: {
          model: () => signal<VM>(null!)
        }
      },
      {
        path: ':id',
        loadComponent: () => import('@shared/forms/form-template').then(c => c.FormTemplate)
      }
    ]
  });
}

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

export function routeTemplate<T>(params: {
  path?: string;
  providers: Array<Provider | EnvironmentProviders>;
  serviceProvider: () => T;
  children: Routes;
}): Route {
  return {
    path: params.path ?? '',
    providers: [
      params.providers,
      provideFeatureTemplate<T>(params.serviceProvider),
    ],
    loadComponent: () =>
      import('app/shared/templates/feature-template').then(
        (e) => e.FeatureTemplate,
      ),
    children: params.children,
  };
}
