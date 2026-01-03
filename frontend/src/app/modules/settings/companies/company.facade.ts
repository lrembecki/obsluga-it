import { ApiFacade } from '@core/interfaces/facade.interface';
import { CompanyVM } from './company.vm';

export class SettingsCompanyFacade extends ApiFacade<CompanyVM> {
  constructor() {
    super([], 'settings/companies');
  }

  protected override withData(data: CompanyVM[]): CompanyVM[] {
    return data
      .map((company) => new CompanyVM(company))
      .sort((a, b) => (a.name || '').localeCompare(b.name || ''));
  }
}
