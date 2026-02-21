import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { AccountSubscriptionVM } from './account-subscription.vm';

export class SecuritySubscriptionAccountFacade extends ArrayApiFacade<AccountSubscriptionVM> {
  constructor() {
    super([], 'account/subscription-accounts');
  }

  protected override withData(data: AccountSubscriptionVM[]): AccountSubscriptionVM[] {
    return data.sort((a, b) => a.name.localeCompare(b.name));
  }
}
