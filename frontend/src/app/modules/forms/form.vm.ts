export class FormVM {
  id: string = null!;
  formDefinitionId: string = null!;
  name: string = null!;
  createdAt: Date = null!;

  constructor(init?: Partial<FormVM>) {
    Object.assign(this, init);

    if (this.createdAt) this.createdAt = new Date(this.createdAt);
  }
}
