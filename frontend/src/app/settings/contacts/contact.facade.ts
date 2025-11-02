import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { ContactModel } from './contact.model';

export class ContactsFacade extends ApiFacade<ContactModel[]> {
  constructor() {
    super([], 'settings/contact');
  }
}
