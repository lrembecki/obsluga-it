import { inject } from '@angular/core';
import { Routes, Route } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { BreadcrumbsService } from '../services/breadcrumbs.service';
import { TranslationService } from '../services/translation.service';

export function withBreadcrumbs(routes: Routes, rootPath = '/'): Routes {
  routes
    .filter((e) => e.data && e.data['label'])
    .forEach((e) => updateBreadcrumbs(e, rootPath));
  return routes;
}

function updateBreadcrumbs(route: Route, rootPath: string): Route {
  const menuItem = <MenuItem>{
    label: route.data ? route.data['label'] : null!,
    routerLink: `${rootPath}${route.path}`,
  };

  route.resolve ??= {};
  route.resolve = {
    ...route.resolve,
    breadcrumb: () => {
      const services = {
        breadcrumbs: inject(BreadcrumbsService),
        translation: inject(TranslationService),
      };
      services.breadcrumbs.add(menuItem);
    },
  };

  route.canDeactivate ??= [];
  route.canDeactivate = [
    ...route.canDeactivate,
    () => {
      inject(BreadcrumbsService).remove(menuItem);
      return true;
    },
  ];

  return route;
}
