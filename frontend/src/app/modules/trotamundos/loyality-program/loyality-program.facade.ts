import { ApiFacade } from '@app/core/interfaces/facade.interface';
import { LoyalityProgramVM } from './loyality-program.vm';

export class TrotamundosLoyalityProgramFacade extends ApiFacade<LoyalityProgramVM> {
  constructor() {
    super([], 'trotamundos/loyality-programs');
  }
}
