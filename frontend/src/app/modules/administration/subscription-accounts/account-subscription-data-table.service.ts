import { DataTableService } from '@app/shared/data-table/data-table.service';
import { AccountSubscriptionVM } from './account-subscription.vm';

export class AdministrationAccountSubscriptionDataTableService extends DataTableService<AccountSubscriptionVM> {
  constructor() {
    super();
    this._schema.set({
      quicksearch: true,
      sortable: true,
      columns: [
        {
          field: 'email',
          label: 'Email',
          renderLink: (record) => ['..', record.id],
          width: '250px',
        },
        { field: 'subscription', label: 'Subscription', width: '250px' },
        { field: 'isDefault', label: 'Default', width: '120px' },
        { field: 'isActive', label: 'Active', width: '120px' },
        {
          field: 'permissionGroups',
          label: 'Groups',
          width: '120px',
          render: (record: AccountSubscriptionVM) =>
            record.permissionGroups.length.toString(),
        },
      ],
      persistenceKey: 'administration-subscription-accounts',
    });
  }
}
