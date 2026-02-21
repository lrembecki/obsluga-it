import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { FormEntryVM } from './form.vm';

export class FormsFacade extends ArrayApiFacade<FormEntryVM> {
  constructor() {
    super([], 'forms');
  }
}
