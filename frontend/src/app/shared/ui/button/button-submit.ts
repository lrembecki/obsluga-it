import { Directive, effect, inject, input } from '@angular/core';
import { TranslationService } from 'app/core/services/translation.service';
import { ButtonSeverity } from 'primeng/button';
import { Button } from './button';

@Directive({
  selector: 'app-button[submit]',
})
export class ButtonSubmit {
  private readonly _button = inject(Button);
  private readonly _translation = inject(TranslationService);

  public readonly text = input(this._translation.instant('Submit'));
  public readonly color = input<ButtonSeverity>('primary');

  constructor() {
    effect(() => this._button.text.set(this.text()));
    effect(() => this._button.color.set(this.color()));
  }
}
