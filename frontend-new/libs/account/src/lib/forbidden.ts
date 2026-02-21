import { Component } from '@angular/core';

@Component({
  selector: 'app-forbidden',
  standalone: true,
  template: `
    <div class="error-container">
      <h1>403 - Forbidden</h1>
      <p>You do not have permission to access this resource.</p>
      <a href="/">Go to Home</a>
    </div>
  `,
  styles: [`
    .error-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      height: 100%;
      gap: 1rem;
    }
    h1 { color: var(--danger, #dc3545); }
    a {
      color: var(--primary);
      text-decoration: none;
    }
    a:hover { text-decoration: underline; }
  `],
})
export class ForbiddenComponent {}
