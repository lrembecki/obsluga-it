import { ArrayApiFacade } from '@obsluga-it/core/facades';
import { AboutUsVM } from './about-us.vm';

export class TrotamundosAboutUsFacade extends ArrayApiFacade<AboutUsVM> {
  constructor() { super([], 'trotamundos/about-us'); }
}
