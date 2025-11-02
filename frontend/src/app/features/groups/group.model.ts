export class GroupModel {
  groupId: string = null!;
  name: string = null!;

  constructor(init?: Partial<GroupModel>) {
    Object.assign(this, init);
  }
}
