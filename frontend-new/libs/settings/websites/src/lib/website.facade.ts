import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { WebsiteVM } from './website.vm';

export class SettingsWebsiteFacade extends ArrayApiFacade<WebsiteVM> {
  constructor() {
    super([], 'settings/websites');
  }

  protected override withData(data: WebsiteVM[]): WebsiteVM[] {
    return data.sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));
  }
}
