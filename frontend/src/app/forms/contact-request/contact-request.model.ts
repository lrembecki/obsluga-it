import { ContactRequestStatus } from './contact-request.status';

export class ContactRequestModel {
  contactRequestId: string = null!;
  dateTime: Date = null!;
  email: string = null!;
  imieNazwisko: string = null!;
  wiadomosc: string = null!;
  status: ContactRequestStatus = null!;

  constructor(init?: Partial<ContactRequestModel>) {
    Object.assign(this, init);

    if (this.dateTime) this.dateTime = new Date(this.dateTime);
  }
}
