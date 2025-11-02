export class TagModel {
  public tagId: string = null!;
  public name: string = null!;

  constructor(init?: Partial<TagModel>) {
    Object.assign(this, init);
  }
}
