import { Component, effect, inject, signal, viewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {
  ToastComponent,
  SidebarComponent,
  BreadcrumbsComponent,
  FooterComponent,
} from '@obsluga-it/shared/ui-kit';
import { AuthFacade } from '@obsluga-it/core/auth';
import { NavbarService } from '@obsluga-it/core/navigation';
import { SignInComponent } from '@obsluga-it/account';
import { modulesRoutes } from './app.routes';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    ToastComponent,
    SidebarComponent,
    BreadcrumbsComponent,
    FooterComponent,
    SignInComponent,
  ],
  styles: `
    :host {
      height: 100dvh;
      display: flex;
      flex-direction: column;
    }

    .app-body {
      flex: 1;
      display: flex;
      margin: 0.5rem;
      gap: 0.5rem;
      overflow: hidden;
    }

    .sidebar-container {
      flex-shrink: 0;
      overflow-y: auto;
      overflow-x: hidden;
      transition:
        width 0.3s ease,
        min-width 0.3s ease;
      border-right: 1px solid var(--border);
      border-radius: 0.25rem;
    }

    .sidebar-container.collapsed {
      width: 3rem;
      min-width: 3rem;
    }

    .sidebar-container:not(.collapsed) {
      width: auto;
      min-width: 220px;
    }

    .app-body-content {
      flex: 1;
      display: flex;
      flex-direction: column;
      overflow-y: auto;
      overflow-x: hidden;
      min-width: 0;
    }

    .content {
      display: flex;
      flex-direction: column;
      flex: auto;
      padding: 0.5rem;
    }
  `,
  template: `
    <ui-toast />

    <div class="app-body">
      @if (isAuthenticated()) {
        <div class="sidebar-container" [class.collapsed]="sidebarCollapsed()">
          <app-sidebar #sidebar />
        </div>
      }
      <div class="app-body-content">
        @if (isAuthenticated()) {
          <app-breadcrumbs />
        }
        <div class="content">
          @if (isAuthenticated()) {
            <router-outlet />
          } @else {
            <app-sign-in />
          }
        </div>
      </div>
    </div>
    <app-footer />
  `,
})
export class AppComponent {
  private readonly _auth = inject(AuthFacade);
  private readonly _navbar = inject(NavbarService);
  protected readonly isAuthenticated = signal(false);
  protected readonly sidebarCollapsed = signal<boolean>(false);
  private readonly sidebar = viewChild<SidebarComponent>('sidebar');

  constructor() {
    effect(() => {
      const session = this._auth.session();
      this.isAuthenticated.set(!!session?.internalJwt);
    });

    effect(() => {
      const sidebarRef = this.sidebar();
      if (sidebarRef) {
        this.sidebarCollapsed.set(sidebarRef.collapsed());
      }
    });
  }

  async ngOnInit() {
    await this._navbar.initialize(modulesRoutes);
  }
}
