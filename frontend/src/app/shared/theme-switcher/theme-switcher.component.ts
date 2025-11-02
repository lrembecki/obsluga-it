import {
  Component,
  ChangeDetectionStrategy,
  inject,
  computed,
} from '@angular/core';
import { ThemeService } from '../../core/services/theme.service';
import { DropdownInputComponent } from '../ui/inputs/dropdown-input.component';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { TranslationService } from 'app/core/services/translation.service';
import { TitleCasePipe } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-theme-switcher',
  imports: [DropdownInputComponent, TranslatePipe],
  providers: [TitleCasePipe],
  template: `
    <div class="theme-switcher">
      <label for="theme-select" class="sr-only">{{
        'THEME.SELECT' | translate
      }}</label>
      <app-dropdown-input
        [data]="themes()"
        textField="name"
        [value]="currentTheme()"
        (valueChange)="onThemeChange($event)"
        [ariaLabel]="'Theme Selector' | translate"
      >
        <ng-template #itemContentTemplate let-item="item">
          {{ 'THEME.' + item.name.toUpperCase() | translate }}
        </ng-template>
      </app-dropdown-input>
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ThemeSwitcherComponent {
  private readonly _theme = inject(ThemeService);
  private readonly _translation = inject(TranslationService);
  private readonly _titlecase = inject(TitleCasePipe);

  themes = computed(() =>
    this._theme.data().map((e) => ({
      name: this._titlecase.transform(e),
      id: this._translation.instant(e),
    })),
  );
  currentTheme = computed(() => {
    const theme = this._theme.theme();
    const existing = this.themes().find(
      (e) => e.id.toString() === theme.toString(),
    );

    return existing;
  });

  onThemeChange(selectedTheme: any) {
    this._theme.setTheme(selectedTheme.id);
  }
}
