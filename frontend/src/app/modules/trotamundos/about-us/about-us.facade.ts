import { ObjectApiFacade } from '@app/core/interfaces/facade.interface';
import { AboutUsVM } from './about-us.vm';

export class TrotamundosAboutUsFacade extends ObjectApiFacade<AboutUsVM> {
  constructor() {
    super(null!, 'trotamundos/pages/about-us');
  }

  protected override withData(entry: AboutUsVM): AboutUsVM {
    return new AboutUsVM(entry);
  }
}
