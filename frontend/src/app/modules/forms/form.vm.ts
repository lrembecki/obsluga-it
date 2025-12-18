export class FormVM {
  id: string = null!;
  name: string = null!;

  constructor(init?: Partial<FormVM>) {
    Object.assign(this, init);
  }
}
