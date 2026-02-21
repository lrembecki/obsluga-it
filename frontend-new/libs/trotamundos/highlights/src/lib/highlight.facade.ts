import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { HighlightVM } from './highlight.vm';

export class TrotamundosHighlightFacade extends ArrayApiFacade<HighlightVM> {
  constructor() { super([], 'trotamundos/highlights'); }
  protected override withData(data: HighlightVM[]): HighlightVM[] {
    return data.sort((a, b) => (a.title ?? '').localeCompare(b.title ?? ''));
  }
}
