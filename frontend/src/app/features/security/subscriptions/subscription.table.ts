import {
  TableColumnModel,
  TableService,
} from 'app/shared/ui/table/table.service';
import { SubscriptionModel } from './subscription.model';
import { effect, inject, Injectable } from '@angular/core';
import { SubscriptionFacade } from '../../../core/facades/subscription.facade';
import { TranslationService } from 'app/core/services/translation.service';

@Injectable()
export class SubscriptionTableService extends TableService<SubscriptionModel> {
  private readonly _translations = inject(TranslationService);
  private readonly _facades = {
    subscriptions: inject(SubscriptionFacade),
  };

  constructor() {
    super();
    effect(() => this.data.set(this._facades.subscriptions.data()));
    effect(() => this.loading.set(this._facades.subscriptions.loading()));

    this.columns.set([
      new TableColumnModel({
        field: 'subscriptionId',
        label: this._translations.instant('Identity'),
      }),
      new TableColumnModel({
        field: 'name',
        label: this._translations.instant('Name'),
      }),
    ]);
  }
}
