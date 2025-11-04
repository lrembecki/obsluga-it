import { Directive, inject, input, output } from '@angular/core';
import { Confirm } from 'app/core/directives/confirm';
import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { TranslationService } from 'app/core/services/translation.service';
import { Button } from './button';

@Directive({
  selector: 'app-button[delete]',
  hostDirectives: [
    {
      directive: Confirm,
      outputs: ['confirmed'], // Map the confirmed output
    },
  ],
})
export class ButtonDelete {
  private readonly _button = inject(Button);
  private readonly _confirm = inject(Confirm);
  private readonly _translation = inject(TranslationService);

  public readonly facade = input<{
    facade: ApiFacade<unknown>;
    identity: string;
  }>(null!);

  public readonly deleted = output<void>();

  ngOnInit() {
    this._confirm.message.set(
      this._translation.instant('Are you sure you want to delete this object?'),
    );

    if (this.facade()) {
      this._confirm.confirmed.subscribe(async () => {
        this._button.isInProgress.set(true);

        const response = await this.facade().facade.delete(
          this.facade().identity,
        );

        this._button.isInProgress.set(false);

        if (response.success) {
          this.deleted.emit();
        }
      });
    } else {
      this.deleted.emit();
    }

    this._button.color.set('danger');
    this._button.text.set(this._translation.instant('Delete'));
  }
}
