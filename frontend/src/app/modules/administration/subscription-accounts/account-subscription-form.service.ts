import { effect, inject } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  CheckboxFormFieldSchema,
  FormSchema,
  MultiSelectFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import { SecurityPermissionGroupFacade } from '../permission-groups/permission-group.provider';
import { AccountSubscriptionVM } from './account-subscription.vm';

export class AdministrationAccountSubscriptionFormService extends FormService<AccountSubscriptionVM> {
  private readonly _facades = {
    permissionGroups: inject(SecurityPermissionGroupFacade),
  };

  constructor() {
    super();

    this._returnRoute.set([
      '/modules/administration/subscription-accounts/list',
    ]);

    effect(() => {
      this._schema.set(
        new FormSchema<AccountSubscriptionVM>({
          fields: [
            new TextFormFieldSchema<AccountSubscriptionVM>({
              key: 'email',
              label: 'Email',
              validators: [Validators.required, Validators.email],
            }),
            new TextFormFieldSchema<AccountSubscriptionVM>({
              key: 'subscription',
              label: 'Subscription',
              validators: [Validators.required],
            }),
            new CheckboxFormFieldSchema<AccountSubscriptionVM>({
              key: 'isActive',
              label: 'Is Active',
            }),
            new CheckboxFormFieldSchema<AccountSubscriptionVM>({
              key: 'isDefault',
              label: 'Is Default',
            }),
            new MultiSelectFormFieldSchema<AccountSubscriptionVM>({
              key: 'permissionGroups',
              label: 'Permission Groups',
              options: this._facades.permissionGroups.data().map((e) => ({
                label: e.name,
                value: e.id,
              })),
            }),
          ],
        }),
      );
    });
  }
}
