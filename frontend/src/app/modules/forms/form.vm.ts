export class FormVM {
  id: string = null!;
  formDefinitionId: string = null!;
  name: string = null!;
  dateTime: Date = null!;

  constructor(init?: Partial<FormVM>) {
    Object.assign(this, init);

    if (this.dateTime) this.dateTime = new Date(this.dateTime);
  }
}
