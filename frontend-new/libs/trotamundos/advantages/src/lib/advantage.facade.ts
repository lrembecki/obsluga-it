import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { AdvantageVM } from './advantage.vm';

export class TrotamundosAdvantageFacade extends ArrayApiFacade<AdvantageVM> {
  constructor() { super([], 'trotamundos/advantages'); }
  protected override withData(data: AdvantageVM[]): AdvantageVM[] {
    return data.sort((a, b) => (a.title ?? '').localeCompare(b.title ?? ''));
  }
}
