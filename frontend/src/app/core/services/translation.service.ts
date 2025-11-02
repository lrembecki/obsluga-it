import { Injectable, signal } from '@angular/core';
import { getCurrentLanguage } from '../providers/translation.providers';

@Injectable()
export class TranslationService {
  private readonly _selectLanguage = signal<string>(getCurrentLanguage());
  private readonly _defaultLanguage = signal<string>('en');
  private readonly _languageDictionary = signal<{
    [language: string]: {
      [key: string]: string;
    };
  }>({});

  public readonly selectLanguage = this._selectLanguage.asReadonly();
  public readonly defaultLanguage = this._defaultLanguage.asReadonly();
  public readonly languageDictionary = this._languageDictionary.asReadonly();

  private initialized = false;
  public async initialize(): Promise<void> {
    if (this.initialized) return;
    this.initialized = true;

    this._selectLanguage.set(getCurrentLanguage());

    const selectedLanguage = this._selectLanguage();
    const defaultLanguage = this._defaultLanguage();

    if (this._selectLanguage()) {
      await this.updateLanguageDictionary(selectedLanguage);
    }

    if (!(defaultLanguage in this._languageDictionary)) {
      await this.updateLanguageDictionary(defaultLanguage);
    }
  }

  private async updateLanguageDictionary(language: string) {
    const data = (await fetch(`./i18n/${language}.json`).then((e) =>
      e.json(),
    )) as {
      [key: string]: string;
    };

    this._languageDictionary.set({
      ...this._languageDictionary(),
      [language]: data ?? {},
    });
  }

  public instant(
    text: string,
    params: { [name: string]: string } = null!,
  ): string {
    return instant(text, params, {
      dictionary: this._languageDictionary(),
      selectedLanguage: this._selectLanguage(),
      defaultLanguage: this._defaultLanguage(),
    });
  }

  public async setLanguage(languageCode: string) {
    await this.updateLanguageDictionary(languageCode);

    localStorage.setItem('language', languageCode);
    this._selectLanguage.set(languageCode);

    window.location.href = window.location.href;
  }
}

function instant(
  text: string,
  params: { [name: string]: string },
  input: {
    dictionary: {
      [language: string]: {
        [key: string]: string;
      };
    };
    defaultLanguage: string;
    selectedLanguage: string;
  },
): string {
  const language = input.selectedLanguage ?? input.defaultLanguage;

  let existing =
    getDeep(text, input.dictionary[language]) ??
    getDeep(text, input.dictionary[input.defaultLanguage]);

  if (!existing) {
    existing = text;
  }

  if (params) {
    Object.keys(params).forEach(
      (key) => (existing = existing.replaceAll(`{{key}}`, params[key])),
    );
  }

  return existing;
}

function getDeep(key: string, dictionary: any) {
  return (key || '')
    .split('.')
    .reduce(
      (acc, item: string) => (!acc ? null : (acc[item] ?? null!)),
      dictionary,
    );
}
