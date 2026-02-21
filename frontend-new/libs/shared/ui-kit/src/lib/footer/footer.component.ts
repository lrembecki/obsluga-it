import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  standalone: true,
  template: `
    <footer class="app-footer">
      <span>&copy; {{ currentYear }} Obsluga IT</span>
    </footer>
  `,
  styles: [`
    .app-footer {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 0.5rem 1rem;
      font-size: 0.8rem;
      opacity: 0.6;
      border-top: 1px solid var(--border);
    }
  `],
})
export class FooterComponent {
  protected readonly currentYear = new Date().getFullYear();
}
