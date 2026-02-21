import { Component, input } from '@angular/core';

@Component({
  selector: 'app-loading',
  standalone: true,
  template: `<p>{{ text() }}</p>`,
  styles: [`
    :host {
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 2rem;
    }
    p {
      font-size: 1.1rem;
      opacity: 0.7;
    }
  `],
})
export class LoadingComponent {
  readonly text = input<string>('Loading...');
}
