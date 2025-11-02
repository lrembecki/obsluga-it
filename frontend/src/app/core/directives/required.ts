import { Directive, effect, inject, input } from '@angular/core';
import { Valid } from './valid';

@Directive({
  selector: '[required]',
})
export class Required {
  private readonly _valid = inject(Valid);
  public readonly value = input.required<unknown>();
  public readonly required = input(true);

  constructor() {
    effect(() => {
      this._valid.valid.set(
        !this.required() ||
          (!!this.value() && (this.value()?.toString() || '').length > 0),
      );
    });
  }
}
