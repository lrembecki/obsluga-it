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
        <p>{{ 'FOOTER.ALL_RIGHTS_RESERVED' | translate }}</p>
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
    .footer {
      display: flex;
      padding: 1rem;
      flex-direction: column;
    }

    .footer-content {
      flex: 1;
      display: flex;
      justify-content: center;
      align-items: center;
      width: 100%;
      font-size: 1rem;
    }

    .footer-switchers {
      display: flex;
      justify-content: center;
      gap: 1rem;
      margin-top: 0.5rem;

      app-theme-switcher,
      app-language-switcher {
        margin-bottom: 0.25rem !important;
      }
    }

    @media (max-width: 600px) {
      .footer {
        padding: 0.25rem;
      }
      .footer-switchers {
        flex-direction: column;
      }
    }
  `,
})
export class FooterComponent {
  private readonly _storage = inject(StorageService);
  protected readonly userData = this._storage.account.data;
}
