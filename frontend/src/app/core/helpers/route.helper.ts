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
import { ActivatedRoute, ActivatedRouteSnapshot, DefaultExport, LoadChildren, Route, Routes } from '@angular/router';
import { provideFeatureTemplate } from 'app/shared/templates/feature-template.service';
import { Observable } from 'rxjs';
import { PermissionType } from '../defaults/permission.default';
import { authorizedGuard } from '../guards/authorized-guard';

export function listRoute<VM, TProvider>(
  providers: (Provider | EnvironmentProviders)[],
  provideModel: (id: string, services: TProvider) => VM,
  serviceProvider: () => TProvider,
  loadListComponent: () => Type<unknown> | Observable<Type<unknown> | DefaultExport<Type<unknown>>> | Promise<Type<unknown> | DefaultExport<Type<unknown>>>,
  loadFormComponent: () => Type<unknown> | Observable<Type<unknown> | DefaultExport<Type<unknown>>> | Promise<Type<unknown> | DefaultExport<Type<unknown>>>
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
      loadComponent: loadFormComponent,
      resolve: {
        model: () => signal<VM>(null!)
      }
    },
    {
      path: ':id',
      loadComponent: loadFormComponent,
      resolve: {
        model: (snapshot: ActivatedRouteSnapshot) => {
          const services = serviceProvider();
          const id = snapshot.params['id'] as string;

            return computed(() => {

              return provideModel(id, services);
            });
        }
      }
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
