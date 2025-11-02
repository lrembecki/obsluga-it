import { Component, input } from '@angular/core';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
  selector: 'app-loading',
  imports: [ProgressSpinnerModule, TranslatePipe],
  template: `
    <div class="loading-overlay">
      <p-progressSpinner
        [style]="{ width: size() + 'px', height: size() + 'px' }"
        strokeWidth="4"
        fill="transparent"
        animationDuration=".8s"
      >
      </p-progressSpinner>
      @if (showText()) {
        <span
          class="loading-text"
          [innerHTML]="(text() | translate) + '...'"
        ></span>
      }
    </div>
  `,
  styles: `
    .loading-overlay {
      position: absolute;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      z-index: 10;
      border-radius: var(--border-radius);
      min-width: 50px;
    }

    .app-light .loading-overlay {
      background: rgba(255, 255, 255, 0.8);
    }

    .app-dark .loading-overlay {
      background: rgba(30, 30, 47, 0.8);
    }

    .loading-text {
      margin-top: 12px;
      font-size: 14px;
      color: var(--txt);
      font-weight: 500;
    }
  `,
})
export class LoadingComponent {
  public readonly size = input<number>(50);
  public readonly text = input<string>('Loading data');
  public readonly showText = input<boolean>(true);
}
