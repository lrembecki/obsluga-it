import { Component, effect, input, model, output } from '@angular/core';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { ButtonModule, ButtonSeverity } from 'primeng/button';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ButtonColor } from './button-color';

@Component({
  selector: 'app-button',
  imports: [ButtonModule, ProgressSpinnerModule, TranslatePipe],
  hostDirectives: [
    {
      directive: ButtonColor,
      inputs: ['color'],
    },
  ],
  template: `
    <button
      pButton
      (click)="onClick($event)"
      [severity]="color()"
      [disabled]="disabled() || isInProgress()"
    >
      @if (isInProgress()) {
        <p-progress-spinner
          [style]="{ width: '16px', height: '16px' }"
          strokeWidth="4"
          animationDuration=".8s"
        />
      }

      @if (icon()) {
        <i [classList]="[icon()]" pButtonIcon></i>
      }

      @if (text()) {
        <span pButtonLabel>{{ text()! | translate }}</span>
      }
    </button>
  `,
  styles: ``,
})
export class Button {
  public readonly text = model<string | undefined>(null!);
  public readonly icon = input<string>();
  public readonly buttonClick = output<ButtonEvent>();
  public readonly color = model<ButtonSeverity>('secondary');
  public readonly disabled = model(false);
  public readonly isInProgress = model(false);

  public readonly assign = input<Button>(null!);

  constructor() {
    effect(() => {
      if (this.assign()) {
        Object.assign(this, this.assign());
      }
    });
  }

  protected onClick(event: Event) {
    this.buttonClick.emit({
      ...event,
      button: this,
    });
  }
}

export class ButtonEvent extends Event {
  button: Button = null!;
}
