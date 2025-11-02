import { EnvironmentProviders, Provider } from '@angular/core';
import { SubscriptionButtonPanelService } from './subscription.button-panel';
import { SubscriptionTableService } from './subscription.table';

export function provideSubscription(): Provider[] | EnvironmentProviders {
  return [SubscriptionButtonPanelService, SubscriptionTableService];
}
