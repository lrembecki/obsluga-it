import { ApiFacade } from '@app/core/interfaces/facade.interface';
import { AdvantageVM } from './advantage.vm';

export class TrotamundosAdvantageFacade extends ApiFacade<AdvantageVM> {
  constructor() {
    super([], 'trotamundos/advantages');
  }
}
