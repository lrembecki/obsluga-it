import { ArrayApiFacade } from '@core/interfaces/facade.interface';
import { EmailTemplateVM } from './email-template.vm';

export class SettingsEmailTemplateFacade extends ArrayApiFacade<EmailTemplateVM> {
  constructor() {
    super([], 'settings/email-templates');
  }

  protected override withData(data: EmailTemplateVM[]): EmailTemplateVM[] {
    return data
      .map((e) => new EmailTemplateVM(e))
      .sort((a, b) => (a.name || '').localeCompare(b.name || ''))
      .slice();
  }
}
