export class FormFilterVM {
  formDefinitionId: string = null!;
  createdAt: Partial<{
    from: Date | null;
    to: Date | null;
  }> = {
    from: null,
    to: null,
  };

  constructor(init?: Partial<FormFilterVM>) {
    Object.assign(this, init);

    this.createdAt ??= {};
    this.createdAt.from = Date.readDate(this.createdAt.from);
    this.createdAt.to = Date.readDate(this.createdAt.to);
  }
}
