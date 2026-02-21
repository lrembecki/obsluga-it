export class PermissionGroupVM {
  id: string = null!;
  name: string = null!;
  permissions: string[] = [];

  constructor(init?: Partial<PermissionGroupVM>) {
    Object.assign(this, init);
  }
}
