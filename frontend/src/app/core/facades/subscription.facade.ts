import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { SubscriptionModel } from '../../features/security/subscriptions/subscription.model';

export class SubscriptionFacade extends ApiFacade<SubscriptionModel[]> {
  constructor() {
    super([], 'subscriptions');
  }
}
