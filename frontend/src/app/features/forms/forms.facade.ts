import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { FormsModel } from './models/forms.model';

export class FormsFacade extends ApiFacade<FormsModel> {
  constructor() {
    super([], 'forms');
  }
}
