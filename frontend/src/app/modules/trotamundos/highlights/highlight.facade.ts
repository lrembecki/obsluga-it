import { ApiFacade } from 'app/core/interfaces/facade.interface';
import { HighlightVM } from './highlight.vm';

export class TrotamundosHighlightFacade extends ApiFacade<HighlightVM> {
  constructor() {
    super([], 'trotamundos/highlights');
  }
}
