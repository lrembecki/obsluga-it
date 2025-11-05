import { PermissionGroupVM } from '../permission-groups/permission-group.vm';
import { AccountSubscriptionDTO } from './account-subscription.dto';
import { AccountSubscriptionVM } from './account-subscription.vm';

describe('AccountSubscriptionDTO', () => {
  it('should map and sort permission group ids from VM', () => {
    const groups = [
      new PermissionGroupVM({ id: '2', name: 'Bravo' }),
      new PermissionGroupVM({ id: '1', name: 'Alpha' }),
      new PermissionGroupVM({ id: '3', name: 'Charlie' }),
    ];
    const vm = new AccountSubscriptionVM({
      id: 'sub-1',
      email: 'user@example.com',
      subscription: 'Gold',
      isActive: true,
      isDefault: false,
      permissionGroups: groups,
    });

    const dto = AccountSubscriptionDTO.fromVM(vm);
    expect(dto.permissionGroups).toEqual(['1', '2', '3']);
  });
});
