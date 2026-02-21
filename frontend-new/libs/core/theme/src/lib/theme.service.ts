import { effect, Injectable, signal } from '@angular/core';

export type ThemeType = 'light' | 'dark' | 'system';

const themes: ThemeType[] = ['light', 'dark', 'system'];

@Injectable({ providedIn: 'root' })
export class ThemeService {
  private readonly _theme = signal<ThemeType>(
    (localStorage.getItem('theme') as ThemeType) ?? 'system',
  );
  private readonly _data = signal<ThemeType[]>(themes.map((e) => e));
  public readonly theme = this._theme.asReadonly();
  public readonly data = this._data.asReadonly();

  constructor() {
    effect(() => {
      let theme = this.theme();
      const root = document.documentElement;
      root.classList.remove('app-light', 'app-dark');

      if (theme === 'system') {
        const prefersDark = globalThis.matchMedia(
          '(prefers-color-scheme: dark)',
        ).matches;
        theme = prefersDark ? 'dark' : 'light';
      }

      root.classList.add(`app-${theme}`);
    });
  }

  setTheme(theme: ThemeType) {
    this._theme.set(theme);
    localStorage.setItem('theme', theme);
  }
}
