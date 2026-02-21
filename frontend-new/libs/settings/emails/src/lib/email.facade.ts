import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { EmailVM } from './email.vm';

export class SettingsEmailFacade extends ArrayApiFacade<EmailVM> {
  constructor() {
    super([], 'settings/emails');
  }

  protected override withData(data: EmailVM[]): EmailVM[] {
    return data.sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));
  }
}
