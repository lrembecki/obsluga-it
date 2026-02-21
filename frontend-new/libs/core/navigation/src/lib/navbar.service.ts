import { computed, Injectable, signal } from '@angular/core';
import { Routes } from '@angular/router';

@Injectable()
export class NavbarService {
  public readonly customModules = signal<ModuleItem[]>([]);
  public readonly routeModules = signal<ModuleItem[]>([]);
  public readonly data = computed(() =>
    [...this.customModules(), ...this.routeModules()].sort((a, b) =>
      a.label.localeCompare(b.label),
    ),
  );

  async initialize(): Promise<void> {
    const modules: ModuleItem[] = [];
    const moduleRoutes = await import('../../../modules-routes').then(
      (m) => m.modulesRoutes,
    ).catch(() => [] as Routes);

    for (const moduleRoute of moduleRoutes) {
      if (!moduleRoute.data || !moduleRoute.data['title']) continue;

      const moduleItem = new ModuleItem({
        label: moduleRoute.data['title'],
        path: `modules/${moduleRoute.path}`,
      });

      try {
        const children = (await moduleRoute.loadChildren!()) as Routes;

        for (const childRoute of children.filter(
          (e) => e.data && e.data['label'],
        )) {
          const pageItem = new PageItem({
            label: childRoute.data!['label'] ?? childRoute.path,
            path: `modules/${moduleRoute.path}/${childRoute.path}`,
            permissions: childRoute.data!['permissions'] ?? [],
          });

          moduleItem.items.push(pageItem);
        }
      } catch {
        // Module routes not yet loaded
      }

      if (moduleItem.items.length === 0) {
        continue;
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
  permissions: string[] = [];

  constructor(init?: Partial<PageItem>) {
    Object.assign(this, init);
  }
}
