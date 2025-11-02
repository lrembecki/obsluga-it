import { Component, effect, inject, signal } from '@angular/core';
import { Route, Routes } from '@angular/router';
import { AppUpdateService } from 'app/core/services/app-update.service';
import { AuthService } from 'app/core/services/auth.service';
import { StorageService } from 'app/core/services/storage.service';
import { TranslationService } from 'app/core/services/translation.service';
import * as feature from 'app/features/routes';
import * as forms from 'app/forms/routes';
import * as settings from 'app/settings/routes';

import { MenuItem } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { BadgeModule } from 'primeng/badge';
import { MenuModule } from 'primeng/menu';
import { PanelMenu } from 'primeng/panelmenu';
import { RippleModule } from 'primeng/ripple';

@Component({
  standalone: true,
  imports: [MenuModule, BadgeModule, RippleModule, AvatarModule, PanelMenu],
  selector: 'app-navbar',
  template: `<p-panelmenu
    [model]="items()"
    [multiple]="true"
    class="flex justify-center"
    styleClass="w-full md:w-80"
  />`,
  styles: [
    `
      .p-panelmenu {
        min-width: 200px;
      }
    `,
  ],
})
export class NavbarComponent {
  private readonly _translation = inject(TranslationService);
  private readonly _storage = inject(StorageService);
  private readonly _auth = inject(AuthService);
  private readonly _appUpdate = inject(AppUpdateService);

  items = signal<MenuItem[]>([]);

  constructor() {
    effect(async () => {
      const requestRoutes = await this.getItemGroup(
        'Forms',
        forms.routes,
        'forms/',
      );
      const featureRoutes = await this.getItemGroup('Features', feature.routes);
      const settingsRoutes = await this.getItemGroup(
        'Settings',
        settings.routes,
        'settings/',
      );

      this.items.set([
        ...this.provideIfAny(featureRoutes),
        ...this.provideIfAny(requestRoutes),
        ...this.provideIfAny(settingsRoutes),
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

  private provideIfAny<T extends { items: MenuItem[] }>(item: T): T[] {
    return item.items.length ? [item] : [];
  }

  private async getItemGroup(
    label: string,
    routes: Routes,
    rootPath: string = '',
  ) {
    return {
      label: this._translation.instant(label),
      expanded: true,
      items: [
        ...(this._storage.account.data()?.user.email
          ? await this.getItems(routes, rootPath)
          : []),
      ],
    };
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
    for (let i = 0; i < routes.length; i++) {
      const route = routes[i];

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
}
