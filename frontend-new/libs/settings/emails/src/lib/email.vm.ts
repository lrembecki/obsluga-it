export class EmailVM {
  id: string = null!;
  name: string = null!;
  address: string = null!;

  constructor(init?: Partial<EmailVM>) {
    Object.assign(this, init);
  }
}
