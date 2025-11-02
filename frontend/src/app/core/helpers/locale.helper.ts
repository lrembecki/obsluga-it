import { registerLocaleData } from '@angular/common';

const baseImports: Record<string, () => Promise<any>> = {
  pl: () => import('@angular/common/locales/pl'),
  en: () => import('@angular/common/locales/en'),
  _: () => baseImports['en'](),
};

const extraImports: Record<string, () => Promise<any>> = {
  pl: () => import('@angular/common/locales/extra/pl'),
  en: () => import('@angular/common/locales/extra/en'),
  _: () => extraImports['en'](),
};

export function detectLocale(): string {
  const raw = (
    navigator.languages?.[0] ||
    navigator.language ||
    'en-US'
  ).trim();

  return raw
    .replace('_', '-')
    .replace(/^iw\b/, 'he')
    .replace(/^in\b/, 'id')
    .replace(/^ji\b/, 'yi');
}

export async function loadAndRegisterLocale(id: string): Promise<string> {
  const norm = id.replace('_', '-');
  const lang = norm.split('-')[0].toLowerCase();

  const choose = (k: string) => (baseImports[k] && extraImports[k] ? k : null);
  const target = choose(norm) ?? choose(lang) ?? 'en';

  const [base, extra] = await Promise.all([
    baseImports[target](),
    extraImports[target](),
  ]);
  registerLocaleData(
    base.default || base,
    target,
    (extra as any).default || extra,
  );
  return target;
}
