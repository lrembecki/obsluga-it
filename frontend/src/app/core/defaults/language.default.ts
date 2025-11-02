export const languages = ['en', 'pl'] as const;
export type LanguageType = (typeof languages)[number];
