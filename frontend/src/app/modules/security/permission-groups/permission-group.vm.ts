import { PermissionVM } from '../permissions/permission.vm';

export class PermissionGroupVM {
  id: string = null!;
  name: string = null!;
  permissions: PermissionVM[] = [];

  constructor(init?: Partial<PermissionGroupVM>) {
    Object.assign(this, init);
    this.permissions = init?.permissions?.map(p => new PermissionVM(p)) || [];
  }
}
