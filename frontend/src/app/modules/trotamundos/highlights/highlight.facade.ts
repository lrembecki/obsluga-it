import { ArrayApiFacade } from 'app/core/interfaces/facade.interface';
import { HighlightVM } from './highlight.vm';

export class TrotamundosHighlightFacade extends ArrayApiFacade<HighlightVM> {
  constructor() {
    super([], 'trotamundos/highlights');
  }
}
