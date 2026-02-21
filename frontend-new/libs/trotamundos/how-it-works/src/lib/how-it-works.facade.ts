import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { HowItWorksVM } from './how-it-works.vm';

export class TrotamundosHowItWorksFacade extends ArrayApiFacade<HowItWorksVM> {
  constructor() { super([], 'trotamundos/how-it-works'); }
}
