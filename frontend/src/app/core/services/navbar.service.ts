import { inject, Injectable, signal } from '@angular/core';
import { Routes } from '@angular/router';
import { PermissionType } from '../defaults/permission.default';
import { AuthService } from './auth.service';

@Injectable()
export class NavbarService {
  private readonly _signal = signal<ModuleItem[]>([]);
  public readonly adata = this._signal.asReadonly();
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

      for (const childRoute of children) {
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

      if (moduleItem.items.length === 0) {
        continue;
      }

      modules.push(moduleItem);
    }

    console.log({ modules });
    this._signal.set(modules);
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
