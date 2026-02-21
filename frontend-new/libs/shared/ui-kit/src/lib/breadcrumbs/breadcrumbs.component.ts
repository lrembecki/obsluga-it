import { Component, inject } from '@angular/core';
import { BreadcrumbsService } from '@obsluga-it/core/breadcrumbs';

@Component({
  selector: 'app-breadcrumbs',
  standalone: true,
  template: `
    <nav class="breadcrumbs">
      @for (item of breadcrumbsService.data(); track item.label; let last = $last) {
        @if (item.url && !last) {
          <a [href]="item.url">{{ item.label }}</a>
          <span class="separator">/</span>
        } @else {
          <span class="current">{{ item.label }}</span>
        }
      }
    </nav>
  `,
  styles: [`
    .breadcrumbs {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      padding: 0.5rem;
      font-size: 0.875rem;
    }
    a {
      color: var(--primary);
      text-decoration: none;
    }
    a:hover {
      text-decoration: underline;
    }
    .separator {
      opacity: 0.5;
    }
    .current {
      opacity: 0.7;
    }
  `],
})
export class BreadcrumbsComponent {
  protected readonly breadcrumbsService = inject(BreadcrumbsService);
}
