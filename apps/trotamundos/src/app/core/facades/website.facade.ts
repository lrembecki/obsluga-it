import { inject, signal } from '@angular/core';
import { ApiService } from '../services/api.service';
import { WebsiteVM } from '../models/website.model';
import { lastValueFrom } from 'rxjs';

export class WebsiteFacade {
  private readonly _api = inject(ApiService);
  private readonly _data = signal<WebsiteVM>(null!);

  public readonly data = this._data.asReadonly();

  public async initialize(): Promise<void> {
    const website = await lastValueFrom(this._api.get<WebsiteVM[]>('settings/website'));
    this._data.set(website[0]);
  }
}
