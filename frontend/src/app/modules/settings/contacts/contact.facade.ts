import { ArrayApiFacade } from 'app/core/interfaces/facade.interface';
import { ContactModel } from './contact.model';

export class ContactsFacade extends ArrayApiFacade<ContactModel> {
  constructor() {
    super([], 'settings/contacts');
  }
}
