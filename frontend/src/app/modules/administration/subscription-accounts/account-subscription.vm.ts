import { PermissionGroupVM } from '../permission-groups/permission-group.vm';

export class AccountSubscriptionVM {
  id: string = null!;
  email: string = null!; // account email
  subscription: string = null!; // subscription name/code
  isActive: boolean = false;
  isDefault: boolean = false;
  permissionGroups: PermissionGroupVM[] = [];

  constructor(init?: Partial<AccountSubscriptionVM>) {
    Object.assign(this, init);
    this.permissionGroups = init?.permissionGroups?.map(p => new PermissionGroupVM(p)) || [];
  }
}
