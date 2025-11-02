import {
  EnvironmentProviders,
  inject,
  provideAppInitializer,
  Provider,
} from '@angular/core';
import { TranslationService } from '../services/translation.service';

function initializeApp() {
  const translation = inject(TranslationService);
  return translation.initialize();
}

export function getCurrentLanguage(): string {
  return localStorage.getItem('language') ?? 'en';
}

export function provideTranslation(): (Provider | EnvironmentProviders)[] {
  return [provideAppInitializer(initializeApp), TranslationService];
}
