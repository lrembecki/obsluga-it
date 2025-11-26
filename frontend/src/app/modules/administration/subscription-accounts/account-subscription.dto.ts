import { PermissionGroupVM } from '../permission-groups/permission-group.vm';
import { AccountSubscriptionVM } from './account-subscription.vm';

// NOTE: Backend DTO (C#) does not include id/accountId/subscriptionId;
// we extend for front-end editing consistency.
export class AccountSubscriptionDTO {
  id: string = null!; // optional backend identity (used in update path)
  email: string = null!;
  subscription: string = null!;
  isActive: boolean = false;
  isDefault: boolean = false;
  permissionGroups: string[] = [];

  constructor(init?: Partial<AccountSubscriptionDTO>) {
    Object.assign(this, init);
  }

  static fromVM(vm: AccountSubscriptionVM): AccountSubscriptionDTO {
    const groups = vm.permissionGroups
      .slice()
      .sort((a, b) => a.name.localeCompare(b.name));

    return new AccountSubscriptionDTO({
      id: vm.id,
      email: vm.email,
      subscription: vm.subscription,
      isActive: vm.isActive,
      isDefault: vm.isDefault,
      permissionGroups: groups.map((g) => g.id),
    });
  }

  static toVM(
    dto: AccountSubscriptionDTO,
    allGroups: PermissionGroupVM[],
  ): AccountSubscriptionVM {
    return new AccountSubscriptionVM({
      id: dto.id,
      email: dto.email,
      subscription: dto.subscription,
      isActive: dto.isActive,
      isDefault: dto.isDefault,
      permissionGroups: dto.permissionGroups
        .map((id) => allGroups.find((g) => g.id === id)!)
        .filter(Boolean)
        .sort((a, b) => a.name.localeCompare(b.name)),
    });
  }
}
