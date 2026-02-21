import { Component, inject, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NavbarService } from '@obsluga-it/core/navigation';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  template: `
    <nav class="sidebar" [class.collapsed]="collapsed()">
      <button class="toggle-btn" (click)="toggleCollapse()">
        {{ collapsed() ? '☰' : '✕' }}
      </button>

      @if (!collapsed()) {
        @for (module of navbarService.data(); track module.path) {
          <div class="module-group">
            <span class="module-label">{{ module.label }}</span>
            @for (page of module.items; track page.path) {
              <a
                class="nav-link"
                [routerLink]="'/' + page.path"
                routerLinkActive="active"
              >
                {{ page.label }}
              </a>
            }
          </div>
        }
      }
    </nav>
  `,
  styles: [`
    .sidebar {
      display: flex;
      flex-direction: column;
      padding: 0.5rem;
      min-width: 200px;
    }
    .sidebar.collapsed {
      min-width: auto;
    }
    .toggle-btn {
      background: none;
      border: 1px solid var(--border);
      border-radius: 4px;
      padding: 0.5rem;
      cursor: pointer;
      margin-bottom: 1rem;
    }
    .module-group {
      margin-bottom: 1rem;
    }
    .module-label {
      font-weight: 700;
      font-size: 0.8rem;
      text-transform: uppercase;
      opacity: 0.6;
      margin-bottom: 0.25rem;
      display: block;
    }
    .nav-link {
      display: block;
      padding: 0.4rem 0.75rem;
      text-decoration: none;
      color: inherit;
      border-radius: 4px;
      transition: background 0.15s;
    }
    .nav-link:hover {
      background: var(--hover);
    }
    .nav-link.active {
      background: var(--primary);
      color: white;
    }
  `],
})
export class SidebarComponent {
  protected readonly navbarService = inject(NavbarService);
  public readonly collapsed = signal(false);

  protected toggleCollapse(): void {
    this.collapsed.set(!this.collapsed());
  }
}
