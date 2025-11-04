
export class AccountModel {
  token: string = null!;
  permissions: string[] = [];
  expires: Date = null!;
  created: Date = null!;
  user: {
    id: string;
    email: string;
  } = null!;
  subscription: SubscriptionVM = null!;
  subscriptions: SubscriptionVM[] = [];
  isAuthenticated: boolean = false;

  constructor(init?: Partial<AccountModel>) {
    Object.assign(this, init);

    ['expires', 'created'].forEach((key) => {
      if (init && (init as any)[key]) {
        (init as any)[key] = new Date((init as any)[key]);
      }
    });
  }
}

export class SubscriptionVM {
  id: string = null!;
  name: string = null!;
}