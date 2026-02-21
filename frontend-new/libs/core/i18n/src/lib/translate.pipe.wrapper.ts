import { Pipe, PipeTransform, inject } from '@angular/core';
import { I18nFacade } from './i18n.facade';

@Pipe({ name: 'translate', standalone: true })
export class TranslatePipeWrapper implements PipeTransform {
  private readonly i18n = inject(I18nFacade);

  transform(key: string, params?: object): string {
    return this.i18n.instant(key, params);
  }
}
