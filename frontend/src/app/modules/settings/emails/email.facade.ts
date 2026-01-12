import { ArrayApiFacade } from '@core/interfaces/facade.interface';
import { EmailVM } from './email.vm';

export class SettingsEmailFacade extends ArrayApiFacade<EmailVM> {
  constructor() {
    super([], 'settings/emails');
  }

  protected override withData(data: EmailVM[]): EmailVM[] {
    return data
      .map((e) => new EmailVM(e))
      .sort((a, b) => {
        const byName = (a.smtpServer || '').localeCompare(b.smtpServer || '');
        return byName;
      })
      .slice();
  }
}
