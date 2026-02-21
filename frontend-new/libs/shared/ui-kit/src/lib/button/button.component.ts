import { Component, input, output } from '@angular/core';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'ui-button',
  standalone: true,
  imports: [ButtonModule],
  template: `
    <p-button
      [label]="label()"
      [severity]="severity()"
      [disabled]="disabled()"
      (onClick)="clicked.emit()"
    />
  `,
})
export class ButtonComponent {
  readonly label = input.required<string>();
  readonly severity = input<'primary' | 'secondary' | 'danger'>('primary');
  readonly disabled = input<boolean>(false);
  readonly clicked = output<void>();
}
