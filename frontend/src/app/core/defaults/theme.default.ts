export const themes = ['light', 'dark', 'system'] as const;
export type ThemeType = (typeof themes)[number];
