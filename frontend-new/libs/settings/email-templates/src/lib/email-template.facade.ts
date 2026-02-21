import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { EmailTemplateVM } from './email-template.vm';

export class SettingsEmailTemplateFacade extends ArrayApiFacade<EmailTemplateVM> {
  constructor() {
    super([], 'settings/email-templates');
  }

  protected override withData(data: EmailTemplateVM[]): EmailTemplateVM[] {
    return data.sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));
  }
}
