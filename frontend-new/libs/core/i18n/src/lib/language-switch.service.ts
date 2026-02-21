import { inject, Injectable, signal } from '@angular/core';
import { I18nFacade } from './i18n.facade';

const SUPPORTED_LANGUAGES = ['en', 'pl'] as const;
export type SupportedLanguage = (typeof SUPPORTED_LANGUAGES)[number];

@Injectable({ providedIn: 'root' })
export class LanguageSwitchService {
  private readonly i18n = inject(I18nFacade);

  readonly currentLanguage = signal<SupportedLanguage>('en');

  switchTo(lang: SupportedLanguage): void {
    this.currentLanguage.set(lang);
    this.i18n.setLanguage(lang);
  }

  getSupportedLanguages(): readonly SupportedLanguage[] {
    return SUPPORTED_LANGUAGES;
  }
}
