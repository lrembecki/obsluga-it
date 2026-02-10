import { inject, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';

import { ApiService } from '@core/services/api.service';
import { CompanyVM } from '@core/models/company.model';

export class CompaniesFacade {
  private readonly _api = inject(ApiService);
  private readonly _data = signal<CompanyVM[]>([]);

  public readonly data = this._data.asReadonly();

  public async initialize(): Promise<void> {
    const companies = await lastValueFrom(this._api.get<CompanyVM[]>('settings/companies'));
    this._data.set(companies);
  }
}
