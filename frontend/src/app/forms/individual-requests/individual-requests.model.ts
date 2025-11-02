export class IndividualRequestModel {
  public readonly individualRequestId: string = null!;

  constructor(init?: Partial<IndividualRequestModel>) {
    Object.assign(this, init);
  }
}
