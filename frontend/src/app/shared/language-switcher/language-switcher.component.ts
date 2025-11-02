import {
  Component,
  ChangeDetectionStrategy,
  inject,
  computed,
} from '@angular/core';
import { TranslationService } from 'app/core/services/translation.service';
import { DropdownInputComponent } from '../ui/inputs/dropdown-input.component';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { languages } from 'app/core/defaults/language.default';

@Component({
  selector: 'app-language-switcher',
  imports: [DropdownInputComponent, TranslatePipe],
  template: `
    <div class="language-switcher">
      <label for="language-select" class="sr-only">
        {{ 'LANGUAGE.SELECT' | translate }}</label
      >
      <app-dropdown-input
        [data]="languages()"
        textField="code"
        [value]="currentLanguage()"
        (valueChange)="onLanguageChange($event)"
        [ariaLabel]="'Language Selector' | translate"
      >
        <ng-template #itemContentTemplate let-item="item">
          {{ 'LANGUAGE.' + item.code.toUpperCase() | translate }}
        </ng-template>
      </app-dropdown-input>
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
  styles: `
    ::ng-deep app-dropdown-input {
      display: flex;
      flex-direction: column;

      & > * {
        flex: auto;
      }
    }
  `,
})
export class LanguageSwitcherComponent {
  private readonly _translation = inject(TranslationService);

  languages = computed(() => {
    return languages.map((e) => ({
      code: e,
    }));
  });

  currentLanguage = computed(() => {
    const lang = this._translation.selectLanguage();
    return this.languages().find((e) => e.code === lang);
  });

  onLanguageChange(selectedLanguage: any) {
    this._translation.setLanguage(selectedLanguage.code);
  }
}
