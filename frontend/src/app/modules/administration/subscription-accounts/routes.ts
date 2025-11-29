import { inject } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  CheckboxFormFieldSchema,
  FormSchema,
  TextFormFieldSchema,
} from '@app/shared/forms/form-schema.model';
import { routeFeatureTemplate } from '@core/helpers/route.helper';
import { SecurityPermissionGroupFacade } from '../permission-groups/permission-group.provider';
import { SecurityPermissionFacade } from '../permissions/permission.provider';
import { SecuritySubscriptionAccountFacade } from './account-subscription.provider';
import { AccountSubscriptionVM } from './account-subscription.vm';

export const routes = routeFeatureTemplate<AccountSubscriptionVM>(
  [
    SecuritySubscriptionAccountFacade,
    SecurityPermissionGroupFacade,
    SecurityPermissionFacade,
  ],
  () => ({
    facade: inject(SecuritySubscriptionAccountFacade),
    persistenceKey: 'account-subscription-data-table',
    columns: [
      {
        field: 'email',
        label: 'Email',
        type: 'text',
        width: '250px',
        sortable: true,
      },
      {
        field: 'subscription',
        label: 'Subscription',
        type: 'text',
        width: '250px',
        sortable: true,
      },
      {
        field: 'isDefault',
        label: 'Default',
        type: 'text',
        width: '120px',
        sortable: true,
      },
      {
        field: 'isActive',
        label: 'Active',
        type: 'text',
        width: '120px',
        sortable: true,
      },
      {
        field: 'permissionGroups',
        label: 'Groups',
        type: 'text',
        width: '120px',
        render: (record: AccountSubscriptionVM) =>
          record.permissionGroups.length.toString(),
      },
      { field: 'id', label: 'ID', type: 'text', width: '120px' },
    ],
  }),
  () =>
    new FormSchema<AccountSubscriptionVM>({
      fields: [
        new TextFormFieldSchema({
          key: 'email',
          label: 'Email',
          placeholder: '',
          validators: [Validators.required, Validators.email],
        }),
        new TextFormFieldSchema({
          key: 'subscription',
          label: 'Subscription',
          placeholder: '',
          validators: [Validators.required],
        }),
        new CheckboxFormFieldSchema({
          key: 'isActive',
          label: 'Is Active',
          placeholder: '',
        }),
        new CheckboxFormFieldSchema({
          key: 'isDefault',
          label: 'Is Default',
          placeholder: '',
        }),
      ],
    }),
);
