import { ArrayApiFacade } from '@app/core/interfaces/facade.interface';
import { FormVM } from './form.vm';

export class FormsFacade extends ArrayApiFacade<FormVM> {
  constructor() {
    super([], 'forms/forms');

    this._requiresInitialization.set(false);
  }

  protected override withData(data: FormVM[]): FormVM[] {
    return data.map((e) => new FormVM(e)).slice();
  }
}
