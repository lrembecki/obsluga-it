import { Component, effect, inject, signal, viewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { AppUpdateService } from './core/services/app-update.service';
import { AuthService } from './core/services/auth.service';
import { NavbarService } from './core/services/navbar.service';
import { StorageService } from './core/services/storage.service';
import { TranslationService } from './core/services/translation.service';
import { SignIn } from './features/account/sign-in';
import { BreadcrumbsComponent } from './shared/ui/breadcrumbs/breadcrumbs.component';
import { FooterComponent } from './shared/ui/footer/footer.component';
import { SidebarComponent } from './shared/ui/navbar/sidebar.component';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    SidebarComponent,
    FooterComponent,
    BreadcrumbsComponent,
    SignIn,
    ToastModule,
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
  providers: [NavbarService],
  template: `
    <p-toast />

    <div class="app-body">
      @if (account()) {
        <div class="sidebar-container" [class.collapsed]="sidebarCollapsed()">
          <app-sidebar #sidebar />
        </div>
      }
      <div class="app-body-content">
        @if (account()) {
          <app-breadcrumbs />
        }
        <div class="content">
          @if (account()) {
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
export class App {
  protected readonly account = inject(StorageService).account.data;
  private readonly _translation = inject(TranslationService);
  private readonly _navbar = inject(NavbarService);
  private readonly _auth = inject(AuthService);
  protected readonly sidebarCollapsed = signal<boolean>(false);
  private readonly sidebar = viewChild<SidebarComponent>('sidebar');

  constructor() {
    // Initialize AppUpdateService to start listening for updates
    inject(AppUpdateService);

    // Sync sidebar collapsed state
    effect(() => {
      const sidebarRef = this.sidebar();
      if (sidebarRef) {
        this.sidebarCollapsed.set(sidebarRef.collapsed());
      }
    });
  }

  async ngOnInit() {
    this._auth.initialize();
    this._translation.initialize();

    await this._navbar.initialize();
  }
}
