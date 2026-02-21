export class AccountSubscriptionVM {
  id: string = null!;
  name: string = null!;
  email: string = null!;

  constructor(init?: Partial<AccountSubscriptionVM>) {
    Object.assign(this, init);
  }
}
