import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { PermissionVM } from './permission.vm';

export class SecurityPermissionFacade extends ArrayApiFacade<PermissionVM> {
  constructor() {
    super([], 'account/permissions');
  }

  protected override withData(data: PermissionVM[]): PermissionVM[] {
    return data.sort((a, b) => a.name.localeCompare(b.name));
  }
}
