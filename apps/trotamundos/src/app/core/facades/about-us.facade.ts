import { inject, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';

import { ApiService } from '@core/services/api.service';
import { AboutUsVM } from '../models/about-us.vm';

export class AboutUsFacade {
  private readonly _api = inject(ApiService);
  private readonly _data = signal<AboutUsVM>(null!);

  public readonly data = this._data.asReadonly();

  public async initialize(): Promise<void> {
    const data = await lastValueFrom(this._api.get<AboutUsVM>('pages/about-us'));
    this._data.set(new AboutUsVM(data));
  }
}
