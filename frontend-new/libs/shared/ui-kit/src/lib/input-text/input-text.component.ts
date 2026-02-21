import { Component, input, model } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'ui-input-text',
  standalone: true,
  imports: [FormsModule, InputTextModule],
  template: `
    <input
      pInputText
      [placeholder]="placeholder()"
      [disabled]="disabled()"
      [(ngModel)]="value"
    />
  `,
})
export class InputTextComponent {
  readonly placeholder = input<string>('');
  readonly disabled = input<boolean>(false);
  readonly value = model<string>('');
}
