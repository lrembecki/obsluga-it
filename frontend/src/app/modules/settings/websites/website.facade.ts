import { ArrayApiFacade } from '@app/core/interfaces/facade.interface';
import { WebsiteVM } from './website.vm';

export class SettingsWebsiteFacade extends ArrayApiFacade<WebsiteVM> {
  constructor() {
    super([], 'settings/websites');
  }

  protected override withData(data: WebsiteVM[]): WebsiteVM[] {
    return data
      .map((website) => new WebsiteVM(website))
      .sort((a, b) => (a.title || '').localeCompare(b.title || ''));
  }
}
