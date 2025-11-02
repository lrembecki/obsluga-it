import { ButtonPanelService } from 'app/shared/ui/button-panel/button-panel';

export class SubscriptionButtonPanelService extends ButtonPanelService {
  constructor() {
    super();
    this.data.set([
      {
        label: this._translation.instant('UI.CREATE'),
      },
    ]);
  }
}
