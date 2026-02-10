import { ObjectApiFacade } from '@app/core/interfaces/facade.interface';
import { IndividualTripVM } from './individual-trip.vm';

export class TrotamundosIndividualTripFacade extends ObjectApiFacade<IndividualTripVM> {
  constructor() {
    super(new IndividualTripVM(), 'trotamundos/individual-trips');
  }

  protected override withData(data: IndividualTripVM): IndividualTripVM {
    return new IndividualTripVM(data);
  }
}
