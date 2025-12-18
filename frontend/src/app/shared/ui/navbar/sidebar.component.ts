import { Component, effect, inject, signal } from '@angular/core';
import { Route, Routes } from '@angular/router';
import { NavbarService } from '@app/core/services/navbar.service';
import { AppUpdateService } from 'app/core/services/app-update.service';
import { AuthService } from 'app/core/services/auth.service';
import { StorageService } from 'app/core/services/storage.service';
import { TranslationService } from 'app/core/services/translation.service';

import { MenuItem } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { BadgeModule } from 'primeng/badge';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { PanelMenu } from 'primeng/panelmenu';
import { RippleModule } from 'primeng/ripple';

@Component({
  standalone: true,
  imports: [
    MenuModule,
    BadgeModule,
    RippleModule,
    AvatarModule,
    PanelMenu,
    ButtonModule,
  ],
  selector: 'app-sidebar',
  template: `
    <div class="sidebar-header">
      <button
        pButton
        [icon]="collapsed() ? 'pi pi-angle-right' : 'pi pi-angle-left'"
        (click)="toggleCollapse()"
        [text]="true"
        severity="secondary"
      ></button>
    </div>
    @if (!collapsed()) {
      <p-panelmenu
        [model]="items()"
        [multiple]="true"
        class="flex justify-center"
        styleClass="w-full md:w-80"
      />
    }
  `,
  styles: [
    `
      :host {
        display: flex;
        flex-direction: column;
      }
      .sidebar-header {
        display: flex;
        justify-content: flex-end;
        padding: 0.5rem;
        border-bottom: 1px solid var(--border);
      }
      .p-panelmenu {
        min-width: 200px;
      }
    `,
  ],
})
export class SidebarComponent {
  collapsed = signal<boolean>(false);
  private readonly _navbar = inject(NavbarService);
  private readonly _translation = inject(TranslationService);
  private readonly _storage = inject(StorageService);
  private readonly _auth = inject(AuthService);
  private readonly _appUpdate = inject(AppUpdateService);

  items = signal<MenuItem[]>([]);

  constructor() {
    // Load saved collapse state
    const savedState = localStorage.getItem('sidebar-collapsed');
    if (savedState !== null) {
      this.collapsed.set(savedState === 'true');
    }

    effect(async () => {
      const modules = this._navbar.data();

      this.items.set([
        ...modules.map((m) => ({
          label: this._translation.instant(m.label),
          expanded: true,
          items: m.items.map((i) => ({
            label: this._translation.instant(i.label),
            routerLink: [i.path],
          })),
        })),
        {
          label: this._storage.account.data()?.user.email,
          expanded: true,
          items: [
            {
              label:
                this._translation.instant('UPDATE.CHECK_FOR_UPDATES') ||
                'Check for Updates',
              command: () => this._appUpdate.manualUpdateCheck(),
            },
            {
              label: this._translation.instant('UI.LOGOUT'),
              command: () => this._auth.logout(),
            },
          ],
        },
      ]);
    });
  }

  private async getItems(
    routes: Routes,
    rootPath: string = '',
  ): Promise<MenuItem[]> {
    const menuItems = Array<MenuItem>();
    routes = routes.filter((e) => e.data && e.data['label']);

    await this.buildMenuItemsFromRoutes(routes, rootPath, menuItems);

    return [...menuItems];
  }

  private async buildMenuItemsFromRoutes(
    routes: Routes,
    rootPath: string,
    menuItems: MenuItem[],
  ) {
    for (const route of routes) {
      if (this.shouldSkipRoute(route)) continue;

      const menuItem = <MenuItem>{
        label: this._translation.instant(route.data!['label']),
        routerLink: `${rootPath}${route.path}`,
      };

      await this.loadChildRoutes(route, menuItem);

      this.addMenuItemIfValid(route, menuItem, menuItems);
    }
  }

  private addMenuItemIfValid(
    route: Route,
    menuItem: MenuItem,
    menuItems: MenuItem[],
  ) {
    console.log({ route, menuItem });

    if (
      !route.loadChildren ||
      menuItem.items!.length > 0 ||
      route.data!['permissions']
    ) {
      menuItems.push(menuItem);
    }
  }

  private shouldSkipRoute(route: Route) {
    let skip = !!route.data;
    skip &&= route.data!['permissions'];
    skip &&= !this._auth.isInRole(route.data!['permissions']);
    return skip;
  }

  private async loadChildRoutes(route: Route, menuItem: MenuItem) {
    if (route.loadChildren) {
      const childrens = route.loadChildren
        ? ((await route.loadChildren!()) as Routes)
        : null;

      menuItem.expanded = true;
      menuItem.items = await this.getItems(
        childrens!,
        `${menuItem.routerLink}/`,
      );
    }

    if (route.children) {
      menuItem.expanded = true;
      menuItem.items = await this.getItems(
        route.children,
        `${menuItem.routerLink}/`,
      );
    }
  }

  toggleCollapse(): void {
    this.collapsed.update((v) => !v);
    localStorage.setItem('sidebar-collapsed', String(this.collapsed()));
  }
}
