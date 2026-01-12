import { ArrayApiFacade } from '@app/core/interfaces/facade.interface';
import { AdvantageVM } from './advantage.vm';

export class TrotamundosAdvantageFacade extends ArrayApiFacade<AdvantageVM> {
  constructor() {
    super([], 'trotamundos/advantages');
  }
}
