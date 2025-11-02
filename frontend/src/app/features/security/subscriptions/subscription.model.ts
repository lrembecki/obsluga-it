export class SubscriptionModel {
  id: string = null!;
  name: string = null!;

  constructor(init?: Partial<SubscriptionModel>) {
    Object.assign(this, init);
  }
}
