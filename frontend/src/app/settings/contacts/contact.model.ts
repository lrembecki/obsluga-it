export class ContactModel {
  contactId: string = null!;
  name: string = null!;
  isActive: boolean = false;
  order: number = null!;
  email: string = null!;
  phone: string = null!;
  position: string = null!;

  constructor(init?: Partial<ContactModel>) {
    Object.assign(this, init);
  }
}
