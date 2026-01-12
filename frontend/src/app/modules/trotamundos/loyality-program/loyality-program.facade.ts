import { ArrayApiFacade } from '@app/core/interfaces/facade.interface';
import { LoyalityProgramVM } from './loyality-program.vm';

export class TrotamundosLoyalityProgramFacade extends ArrayApiFacade<LoyalityProgramVM> {
  constructor() {
    super([], 'trotamundos/loyality-programs');
  }
}
