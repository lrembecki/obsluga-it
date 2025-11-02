import { Component, inject } from '@angular/core';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { AuthService } from 'app/core/services/auth.service';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-unauthorized',
  imports: [TranslatePipe, ButtonModule, CardModule],
  template: `
    <div class="unauthorized-container">
      <p-card
        [header]="'UNAUTHORIZED.TITLE' | translate"
        [subheader]="'UNAUTHORIZED.SUBTITLE' | translate"
        styleClass="unauthorized-card"
      >
        <div class="unauthorized-content">
          <p>{{ 'UNAUTHORIZED.MESSAGE' | translate }}</p>
          <p class="email-warning">
            {{ 'UNAUTHORIZED.EMAIL_NOT_REGISTERED' | translate }}
          </p>

          <div class="button-group">
            <p-button
              [label]="'UNAUTHORIZED.LOGOUT' | translate"
              severity="secondary"
              (onClick)="logout()"
            >
            </p-button>
          </div>
        </div>
      </p-card>
    </div>
  `,
  styles: `
    .unauthorized-container {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 100vh;
      padding: 1rem;
      background: var(--surface-ground);
    }

    .unauthorized-card {
      width: 100%;
      max-width: 400px;
    }

    .unauthorized-content {
      text-align: center;
    }

    .unauthorized-content p {
      margin-bottom: 2rem;
      color: var(--text-color-secondary);
    }

    .button-group {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .button-group p-button {
      width: 100%;
    }

    .email-warning {
      color: var(--red-500);
      font-weight: 500;
      margin-bottom: 1.5rem;
    }
  `,
})
export class Unauthorized {
  private readonly _auth = inject(AuthService);

  logout(): void {
    this._auth.logout();
  }
}
