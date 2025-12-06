import { Component, inject } from '@angular/core';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { StorageService } from 'app/core/services/storage.service';
import { LanguageSwitcherComponent } from '../../language-switcher/language-switcher.component';
import { SubscriptionSwitcher } from '../../subscription-switcher/subscription-switcher.component';
import { ThemeSwitcherComponent } from '../../theme-switcher/theme-switcher.component';

@Component({
  standalone: true,
  selector: 'app-footer',
  imports: [
    ThemeSwitcherComponent,
    TranslatePipe,
    LanguageSwitcherComponent,
    SubscriptionSwitcher,
  ],
  template: `
    <footer class="footer">
      <div class="footer-content">
        <p>
          {{ 'FOOTER.ALL_RIGHTS_RESERVED' | translate }} © {{ currentYear }}
        </p>
      </div>
      <div class="footer-switchers">
        <app-theme-switcher />
        <app-language-switcher />
        @if (userData()?.subscriptions?.length) {
          <app-subscription-switcher />
        }
      </div>
    </footer>
  `,
  styles: `
    @use 'app/styles/variables' as variables;

    ::ng-deep app-footer,
    ::ng-deep app-footer .p-select-label {
      font-size: 0.75rem !important;
    }

    :host {
      display: block;
    }

    .footer {
      display: flex;
      padding: 0.5rem 1rem;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      border-top: 1px solid var(--border);
      background-color: var(--bg-alt);
      color: var(--txt);
      flex-shrink: 0;
      gap: 1rem;
      transition:
        background-color 0.2s ease,
        color 0.2s ease,
        border-color 0.2s ease;
    }

    .footer-content {
      display: flex;
      align-items: center;
      font-size: 0.875rem;
      color: var(--muted);

      p {
        margin: 0;
        transition: color 0.2s ease;
      }
    }

    ::ng-deep .footer-switchers {
      display: flex;
      align-items: center;
      gap: 0.5rem;

      & {
        label,
        .p-dropdown,
        .p-select,
        .p-dropdown-label,
        .p-select-label {
          font-size: 0.75rem;
        }

        .p-dropdown,
        .p-select {
          min-width: auto;
        }
      }
    }

    @media (max-width: 768px) {
      .footer {
        flex-direction: column;
        padding: 0.5rem;
        gap: 0.5rem;
      }

      .footer-content {
        order: 2;
      }

      .footer-switchers {
        order: 1;
        flex-wrap: wrap;
        justify-content: center;
      }
    }
  `,
})
export class FooterComponent {
  private readonly _storage = inject(StorageService);
  protected readonly userData = this._storage.account.data;
  protected readonly currentYear = new Date().getFullYear();
}
