import { inject, Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class I18nFacade {
  private readonly translate = inject(TranslateService);

  setLanguage(lang: string): void {
    this.translate.use(lang);
  }

  instant(key: string, params?: object): string {
    return this.translate.instant(key, params);
  }

  get(key: string, params?: object): Observable<string> {
    return this.translate.get(key, params);
  }
}
