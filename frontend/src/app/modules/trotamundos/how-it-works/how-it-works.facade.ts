import { ObjectApiFacade } from '@app/core/interfaces/facade.interface';
import { HowItWorksVM } from './how-it-works.vm';

export class TrotamundosHowItWorksFacade extends ObjectApiFacade<HowItWorksVM> {
  constructor() {
    super(null!, 'trotamundos/pages/how-it-works');
  }

  protected override withData(entry: HowItWorksVM): HowItWorksVM {
    return new HowItWorksVM(entry);
  }
}
