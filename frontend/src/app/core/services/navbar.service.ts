import { computed, inject, Injectable, signal } from '@angular/core';
import { Routes } from '@angular/router';
import { PermissionType } from '../defaults/permission.default';
import { AuthService } from './auth.service';

@Injectable()
export class NavbarService {
  public readonly customModules = signal<ModuleItem[]>([]);
  public readonly routeModules = signal<ModuleItem[]>([]);
  public readonly data = computed(() =>
    [...this.customModules(), ...this.routeModules()].sort((a, b) =>
      a.label.localeCompare(b.label),
    ),
  );
  private readonly _auth = inject(AuthService);

  async initialize(): Promise<void> {
    const modules: ModuleItem[] = [];
    const moduleRoutes = await import('app/modules/routes').then(
      (m) => m.routes,
    );

    for (const moduleRoute of moduleRoutes) {
      const moduleItem = new ModuleItem({
        label: moduleRoute.data!['title'],
        path: `modules/${moduleRoute.path}`,
      });

      const children = (await moduleRoute.loadChildren!()) as Routes;

      for (const childRoute of children.filter(
        (e) => e.data && e.data['label'],
      )) {
        const pageItem = new PageItem({
          label: childRoute.data!['label'] ?? childRoute.path,
          path: `modules/${moduleRoute.path}/${childRoute.path}`,
          permissions: childRoute.data!['permissions'] ?? [],
        });

        if (pageItem.permissions.length > 0) {
          const hasPermission = this._auth.isInRole(pageItem.permissions);
          if (!hasPermission) {
            continue;
          }
        }

        moduleItem.items.push(pageItem);
      }

      modules.push(moduleItem);
    }

    this.routeModules.set(modules);
  }
}

export class ModuleItem {
  path: string = null!;
  label: string = null!;
  items: PageItem[] = [];

  constructor(init?: Partial<ModuleItem>) {
    Object.assign(this, init);
  }
}

export class PageItem {
  path: string = null!;
  label: string = null!;
  permissions: PermissionType[] = [];

  constructor(init?: Partial<PageItem>) {
    Object.assign(this, init);
  }
}
