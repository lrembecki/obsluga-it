import { Component, inject } from '@angular/core';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { AuthService } from 'app/core/services/auth.service';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-sign-in',
  template: `
    <div class="sign-in-container">
      <p-card
        [header]="'SIGN_IN_TITLE' | translate"
        [subheader]="'SIGN_IN_SUBTITLE' | translate"
        styleClass="sign-in-card"
      >
        <div class="sign-in-content">
          <p>{{ 'SIGN_IN_DESCRIPTION' | translate }}</p>

          <p-button
            [label]="'SIGN_IN_BUTTON' | translate"
            severity="primary"
            size="large"
            styleClass="w-full"
            [loading]="isLoading"
            (onClick)="signIn()"
          >
          </p-button>
        </div>
      </p-card>
    </div>
  `,
  styles: [
    `
      :host {
        flex: auto;
        display: flex;
        flex-direction: column;
      }
      .sign-in-container {
        display: flex;
        flex: auto;
        justify-content: center;
        align-items: center;
        padding: 1rem;
        background: var(--surface-ground);
      }

      .sign-in-card {
        width: 100%;
        max-width: 400px;
      }

      .sign-in-content {
        text-align: center;
      }

      .sign-in-content p {
        margin-bottom: 2rem;
        color: var(--text-color-secondary);
      }
    `,
  ],
  imports: [TranslatePipe, ButtonModule, CardModule],
})
export class SignIn {
  private readonly _authService = inject(AuthService);

  public isLoading = false;

  public async signIn(): Promise<void> {
    this.isLoading = true;
    try {
      await this._authService.authenticate();
    } catch (error) {
      console.error('Sign in failed:', error);
    } finally {
      this.isLoading = false;
    }
  }
}
