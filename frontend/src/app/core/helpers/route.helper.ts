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
import {
  DataTableSchema,
  DataTableSchemaScope,
} from '@app/shared/data-table/data-table.types';
import {
  FormFacadeScope,
  FormReturnRouteScope,
  FormSchema,
  FormSchemaScope,
} from '@app/shared/forms/form-schema.model';
import { provideFeatureTemplate } from '@shared/templates/feature-template.service';
import { Observable } from 'rxjs';
import { PermissionType } from '../defaults/permission.default';
import { authorizedGuard } from '../guards/authorized-guard';

export function routeFeatureTemplate<T>(
  deps: (Provider | EnvironmentProviders)[],
  dataTableFactory: () => DataTableSchema<T>,
  formsFactory: () => FormSchema<T>,
): Routes {
  const featureRoute = routeFeature(...deps);

  routeList<T>(deps, dataTableFactory, featureRoute);
  routeForm<T>(deps, formsFactory, featureRoute);

  return [featureRoute];
}

export function routeFeature(
  ...providers: (Provider | EnvironmentProviders)[]
): Route {
  return {
    path: '',
    providers,
  };
}

export function routeForm<T>(
  deps: (Provider | EnvironmentProviders)[],
  schemaFactory: () => FormSchema<T>,
  route?: Route,
): Route {
  route ??= { path: '' };

  route.providers ??= [];

  route.providers.push(
    {
      provide: FormSchemaScope,
      useFactory: schemaFactory,
      deps,
    },
    {
      provide: FormReturnRouteScope,
      useValue: ['/modules/trotamundos/advantages'],
    },
    { provide: FormFacadeScope, useExisting: deps[0] },
  );

  route.children ??= [];
  route.children.push(
    {
      path: 'create',
      loadComponent: () =>
        import('app/shared/forms/form-template').then((e) => e.FormTemplate),
    },
    {
      path: ':id',
      loadComponent: () =>
        import('app/shared/forms/form-template').then((e) => e.FormTemplate),
    },
  );

  return route;
}

export function routeList<T>(
  deps: (Provider | EnvironmentProviders)[],
  useFactory: () => DataTableSchema<T>,
  route?: Route,
): Route {
  route ??= { path: '' };

  route.providers ??= [];
  route.providers.push(
    {
      provide: DataTableSchemaScope,
      useFactory,
      deps,
    },
    ...deps,
  );

  route.children ??= [];
  route.children.push(
    {
      path: '',
      redirectTo: 'list',
      pathMatch: 'full',
    },
    {
      path: 'list',
      loadComponent: () =>
        import('@app/shared/data-table/data-table.template').then(
          (e) => e.DataTableTemplate,
        ),
    },
  );

  return route;
}

export function listRoute<VM, TProvider>(
  providers: (Provider | EnvironmentProviders)[],
  serviceProvider: () => TProvider,
  loadListComponent: () =>
    | Type<unknown>
    | Observable<Type<unknown> | DefaultExport<Type<unknown>>>
    | Promise<Type<unknown> | DefaultExport<Type<unknown>>>,
): Route {
  return routeTemplate({
    providers: [providers],
    serviceProvider,
    children: [
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full',
      },
      {
        path: 'list',
        loadComponent: loadListComponent,
      },
      {
        path: 'create',
        loadComponent: () =>
          import('@shared/forms/form-template').then((c) => c.FormTemplate),
        resolve: {
          model: () => signal<VM>(null!),
        },
      },
      {
        path: ':id',
        loadComponent: () =>
          import('@shared/forms/form-template').then((c) => c.FormTemplate),
      },
    ],
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
