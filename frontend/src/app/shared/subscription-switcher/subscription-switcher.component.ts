import { TitleCasePipe } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject,
} from '@angular/core';
import { SubscriptionFacade } from 'app/core/facades/subscription.facade';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { AuthService } from 'app/core/services/auth.service';
import { StorageService } from 'app/core/services/storage.service';
import { SubscriptionModel } from 'app/features/security/subscriptions/subscription.model';
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
  private readonly _subscriptions = inject(SubscriptionFacade);
  private readonly _auth = inject(AuthService);

  userData = inject(StorageService).account.data;
  subscriptions = this._subscriptions.data;
  currentSubscription = computed(() =>
    this.subscriptions().find(
      (e) => e.id === this.userData()?.subscription?.id,
    ),
  );

  async ngOnInit() {
    await this._subscriptions.initialize();
  }

  async onChange(event: SubscriptionModel): Promise<void> {
    await this._auth.changeSubscription(event.id);
    window.location.href = window.location.href;
  }
}
