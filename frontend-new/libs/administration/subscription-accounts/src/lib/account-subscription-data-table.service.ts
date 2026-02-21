import { DataTableService } from '@obsluga-it/shared/data-table';
import { AccountSubscriptionVM } from './account-subscription.vm';

export class AdministrationSubscriptionAccountDataTableService extends DataTableService<AccountSubscriptionVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          field: 'name',
          label: 'Name',
          renderLink: (record) => ['..', record.id],
        },
        {
          field: 'email',
          label: 'Email',
        },
      ],
      persistenceKey: 'administration-subscription-accounts',
    });
  }
}
