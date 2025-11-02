export class LanguageModel {
  public code: string = null!;
  public displayName: string = null!;

  constructor(init?: Partial<LanguageModel>) {
    Object.assign(this, init);
  }
}
