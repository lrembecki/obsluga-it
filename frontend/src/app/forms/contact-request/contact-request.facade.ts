import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { ContactRequestModel } from './contact-request.model';

export class ContactRequestFacade extends ApiFacade<ContactRequestModel> {
  constructor() {
    super([], 'requests/contact');

    this._filter.set({
      status: ['new', 'active'],
    });
  }

  protected override withData(
    data: ContactRequestModel[],
  ): ContactRequestModel[] {
    return data.map((e) => new ContactRequestModel(e));
  }
}
