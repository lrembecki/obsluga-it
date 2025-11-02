export class FormControlModel {
  public readonly name: string = null!;
  public readonly type: string = null!;
  public readonly required: boolean = false;
  public readonly defaultValue: string = null!;

  constructor(init?: Partial<FormControlModel>) {
    Object.assign(this, init);
  }
}
