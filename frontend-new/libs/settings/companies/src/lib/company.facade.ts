import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { CompanyVM } from './company.vm';

export class SettingsCompanyFacade extends ArrayApiFacade<CompanyVM> {
  constructor() {
    super([], 'settings/companies');
  }

  protected override withData(data: CompanyVM[]): CompanyVM[] {
    return data.sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));
  }
}
