import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { ContactVM } from './contact.vm';

export class ContactsFacade extends ArrayApiFacade<ContactVM> {
  constructor() {
    super([], 'settings/contacts');
  }

  protected override withData(data: ContactVM[]): ContactVM[] {
    return data.sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));
  }
}
