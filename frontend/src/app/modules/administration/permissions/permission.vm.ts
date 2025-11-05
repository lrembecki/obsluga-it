export class PermissionVM {
  id: string = null!;
  name: string = null!;

  constructor(init?: Partial<PermissionVM>) {
    Object.assign(this, init);
  }
}
