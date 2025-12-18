import { ApiFacade } from '@app/core/interfaces/facade.interface';
import { FormVM } from './form.vm';

export class FormsFacade extends ApiFacade<FormVM> {
  constructor() {
    super([], 'forms/forms');
  }

  protected override withData(data: FormVM[]): FormVM[] {
    return data.map((e) => new FormVM(e)).slice();
  }
}
