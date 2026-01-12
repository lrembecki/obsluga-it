import { effect, inject, signal } from '@angular/core';
import {
  ArrayApiFacade,
  facadeScope,
  ObjectApiFacade,
} from '@app/core/interfaces/facade.interface';

export class FormTemplateModelProvider<T> {
  public readonly model = signal<T>(null!);
  public readonly mode = signal<'create' | 'edit'>('create');
  public readonly id = signal<string>(null!);
}

export class ArrayFormTemplateModelProvider<
  T,
> extends FormTemplateModelProvider<T> {
  public readonly facade = inject(ArrayApiFacade);

  constructor() {
    super();

    effect(() => {
      this.model.set(this.facade.data().find((e) => e.id === this.id()));
      this.mode.set(this.id() ? 'edit' : 'create');
    });

    effect(() =>
      console.log({ model: this.model(), id: this.id(), mode: this.mode() }),
    );
  }
}

export class ObjectFormTemplateModelProvider<
  T,
> extends FormTemplateModelProvider<T> {
  public readonly facade = inject(facadeScope) as ObjectApiFacade<T>;
  constructor() {
    super();
    effect(() => {
      this.model.set(this.facade.data());
      this.mode.set('edit');
    });
  }
}
