import {
  computed,
  contentChildren,
  Directive,
  effect,
  model,
  OnDestroy,
  output,
  OutputRefSubscription,
  signal,
} from '@angular/core';

@Directive({
  selector: '[validate]',
  exportAs: 'validate',
})
export class Valid implements OnDestroy {
  public readonly valid = model<boolean>(true);
  public readonly isValid = computed(
    () => this.isChildrenValid() && this.valid(),
  );
  public readonly isValidChange = output<boolean>();

  protected readonly isChildrenValid = signal(true);
  protected readonly validChildren = contentChildren<Valid>(Valid, {
    descendants: true,
  });

  private _subscriptions: OutputRefSubscription[] = [];

  constructor() {
    effect(() => {
      this.isValidChange.emit(this.isValid());
    });
    effect(() => {
      const inputs = this.validChildren().slice();
      this._subscriptions = inputs.map((e) =>
        e.isValidChange.subscribe(() => this.validate(inputs)),
      );
    });
  }

  ngOnDestroy(): void {
    this._subscriptions.forEach((e) => e.unsubscribe());
  }

  private validate(inputs: Valid[]): void {
    const inputsValidity = inputs.filter((e) => !e.isValid()).length === 0;
    this.isChildrenValid.set(inputsValidity);
    this.isValidChange.emit(this.isValid());
  }
}
