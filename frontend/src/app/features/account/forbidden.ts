import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { AuthService } from 'app/core/services/auth.service';

@Component({
  selector: 'app-forbidden',
  imports: [TranslatePipe, ButtonModule, CardModule],
  template: `
    <div class="forbidden-container">
      <p-card
        [header]="'FORBIDDEN.TITLE' | translate"
        [subheader]="'FORBIDDEN.SUBTITLE' | translate"
        styleClass="forbidden-card"
      >
        <div class="forbidden-content">
          <p>{{ 'FORBIDDEN.MESSAGE' | translate }}</p>

          <div class="button-group">
            <p-button
              [label]="'FORBIDDEN.GO_BACK' | translate"
              severity="primary"
              (onClick)="goBack()"
            >
            </p-button>

            <p-button
              [label]="'FORBIDDEN.GO_HOME' | translate"
              severity="secondary"
              (onClick)="goHome()"
            >
            </p-button>

            <p-button
              [label]="'FORBIDDEN.SIGN_OUT' | translate"
              severity="danger"
              [outlined]="true"
              (onClick)="signOut()"
            >
            </p-button>
          </div>
        </div>
      </p-card>
    </div>
  `,
  styles: `
    .forbidden-container {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 100vh;
      padding: 1rem;
      background: var(--surface-ground);
    }

    .forbidden-card {
      width: 100%;
      max-width: 400px;
    }

    .forbidden-content {
      text-align: center;
    }

    .forbidden-content p {
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
  `,
})
export class Forbidden {
  private readonly _router = inject(Router);
  private readonly _authService = inject(AuthService);

  goBack(): void {
    window.history.back();
  }

  goHome(): void {
    this._router.navigate(['/']);
  }

  async signOut(): Promise<void> {
    await this._authService.logout();
  }
}
