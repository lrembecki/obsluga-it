import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, MultiSelectFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { PermissionGroupVM } from './permission-group.vm';

export class AdministrationPermissionGroupFormService extends ArrayFormService<PermissionGroupVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/administration/permission-groups/list']);

    effect(() => {
      this._schema.set(
        new FormSchema<PermissionGroupVM>({
          fields: [
            new TextFormFieldSchema<PermissionGroupVM>({
              key: 'name',
              label: 'Name',
              validators: [Validators.required],
            }),
          ],
        }),
      );
    });
  }
}
