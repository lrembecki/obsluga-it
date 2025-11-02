import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToastModule } from 'primeng/toast';
import { AppUpdateService } from './core/services/app-update.service';
import { AuthService } from './core/services/auth.service';
import { StorageService } from './core/services/storage.service';
import { TranslationService } from './core/services/translation.service';
import { SignIn } from './features/account/sign-in';
import { BreadcrumbsComponent } from './shared/ui/breadcrumbs/breadcrumbs.component';
import { FooterComponent } from './shared/ui/footer/footer.component';
import { NavbarComponent } from './shared/ui/navbar/navbar.component';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    NavbarComponent,
    FooterComponent,
    BreadcrumbsComponent,
    SignIn,
    ToastModule,
  ],
  styles: `
    :host {
      height: 100dvh;
    }

    .app-body-content {
      & > * {
        flex: auto;
      }
    }

    .content {
      display: flex;
      flex-direction: column;
      flex: auto;
    }

    :host,
    .app-body-content {
      display: flex;
      flex: auto;
      flex-direction: column;
      justify-content: flex-start;
      width: 100%;
      overflow-x: hidden;
    }
    .app-body {
      flex: auto;
      display: flex;
    }
  `,
  providers: [],
  template: `
    <p-toast />
    @if (account()) {
      <app-breadcrumbs />
    }

    <div class="app-body" style="margin: .5rem;">
      @if (account()) {
        <app-navbar />
      }
      <div class="app-body-content" style="margin-left: .5rem;">
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
  private readonly _auth = inject(AuthService);

  constructor() {
    // Initialize AppUpdateService to start listening for updates
    inject(AppUpdateService);
  }

  ngOnInit() {
    this._auth.initialize();
    this._translation.initialize();
  }
}
