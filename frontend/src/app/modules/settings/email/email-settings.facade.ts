import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { EmailSettingsModel } from './email-settings.model';

export class EmailSettingsFacade extends ApiFacade<EmailSettingsModel> {
  constructor() {
    super(null!, 'settings/email');
  }
}
