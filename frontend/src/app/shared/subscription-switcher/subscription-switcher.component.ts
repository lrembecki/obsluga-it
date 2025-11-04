import { TitleCasePipe } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
} from '@angular/core';
import { SubscriptionVM } from 'app/core/models/account.model';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { AuthService } from 'app/core/services/auth.service';
import { StorageService } from 'app/core/services/storage.service';
import { DropdownInputComponent } from '../ui/inputs/dropdown-input.component';

@Component({
  standalone: true,
  selector: 'app-subscription-switcher',
  imports: [DropdownInputComponent, TranslatePipe],
  providers: [TitleCasePipe],
  template: `
    <div class="theme-switcher">
      <label for="theme-select" class="sr-only">{{
        'THEME.SELECT' | translate
      }}</label>
      <app-dropdown-input
        [data]="subscriptions()"
        textField="name"
        [value]="currentSubscription()"
        [ariaLabel]="'Subscription Selector' | translate"
        (valueChange)="onChange($event!)"
      >
      </app-dropdown-input>
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SubscriptionSwitcher {
  private readonly _auth = inject(AuthService);

  userData = inject(StorageService).account.data;
  subscriptions = computed(() => this.userData()?.subscriptions || []);
  currentSubscription = computed(() =>
    this.userData()?.subscription ?? null!
  );

  async onChange(event: SubscriptionVM): Promise<void> {
    await this._auth.changeSubscription(event.id);
    window.location.href = window.location.href;
  }
}
