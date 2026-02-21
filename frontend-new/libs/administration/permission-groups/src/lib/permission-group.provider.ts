import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { PermissionGroupVM } from './permission-group.vm';

export class SecurityPermissionGroupFacade extends ArrayApiFacade<PermissionGroupVM> {
  constructor() {
    super([], 'account/permission-groups');
  }

  protected override withData(data: PermissionGroupVM[]): PermissionGroupVM[] {
    return data.sort((a, b) => a.name.localeCompare(b.name));
  }
}
