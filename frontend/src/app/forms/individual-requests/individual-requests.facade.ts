import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { IndividualRequestModel } from './individual-requests.model';

export class IndividualRequestsFacade extends ApiFacade<
  IndividualRequestModel
> {
  constructor() {
    super([], 'requests/individual');

    this._filter.set({
      status: ['new', 'active'],
    });
  }

  protected override withData(
    data: IndividualRequestModel[],
  ): IndividualRequestModel[] {
    return data.map((e) => new IndividualRequestModel(e));
  }
}
