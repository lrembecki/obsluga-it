import { Component, inject } from '@angular/core';
import { AuthFacade } from '@obsluga-it/core/auth';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  template: `
    <div class="sign-in-container">
      <h1>Sign In</h1>
      <p>Please sign in to access the application.</p>
      <button class="btn-sign-in" (click)="signIn()">
        Sign In with Microsoft
      </button>
    </div>
  `,
  styles: [`
    .sign-in-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      height: 100%;
      gap: 1rem;
    }
    h1 {
      font-size: 2rem;
    }
    .btn-sign-in {
      padding: 0.75rem 2rem;
      background: var(--primary, #0078d4);
      color: white;
      border: none;
      border-radius: 4px;
      font-size: 1rem;
      cursor: pointer;
    }
    .btn-sign-in:hover {
      opacity: 0.9;
    }
  `],
})
export class SignInComponent {
  private readonly auth = inject(AuthFacade);

  signIn(): void {
    this.auth.authenticate().subscribe();
  }
}
