import { Directive, input } from '@angular/core';
import { ButtonSeverity } from 'primeng/button';

@Directive({
  selector: 'app-button[color]',
})
export class ButtonColor {
  public readonly color = input<ButtonSeverity>('secondary');
}
