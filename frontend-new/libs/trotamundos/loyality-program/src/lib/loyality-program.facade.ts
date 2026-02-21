import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { LoyalityProgramVM } from './loyality-program.vm';

export class TrotamundosLoyalityProgramFacade extends ArrayApiFacade<LoyalityProgramVM> {
  constructor() { super([], 'trotamundos/loyality-program'); }
  protected override withData(data: LoyalityProgramVM[]): LoyalityProgramVM[] {
    return data.sort((a, b) => (a.name ?? '').localeCompare(b.name ?? ''));
  }
}
