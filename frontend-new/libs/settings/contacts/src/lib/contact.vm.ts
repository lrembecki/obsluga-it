export class ContactVM {
  id: string = null!;
  name: string = null!;
  email: string = null!;
  phone: string = null!;

  constructor(init?: Partial<ContactVM>) {
    Object.assign(this, init);
  }
}
