import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { AccountSubscriptionVM } from './account-subscription.vm';

export class AdministrationSubscriptionAccountFormService extends ArrayFormService<AccountSubscriptionVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/administration/subscription-accounts/list']);

    effect(() => {
      this._schema.set(
        new FormSchema<AccountSubscriptionVM>({
          fields: [
            new TextFormFieldSchema<AccountSubscriptionVM>({
              key: 'name',
              label: 'Name',
              validators: [Validators.required],
            }),
            new TextFormFieldSchema<AccountSubscriptionVM>({
              key: 'email',
              label: 'Email',
              validators: [Validators.required, Validators.email],
            }),
          ],
        }),
      );
    });
  }
}
