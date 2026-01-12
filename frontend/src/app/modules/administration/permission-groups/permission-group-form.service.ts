import { effect, inject } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  FormSchema,
  MultiSelectFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { ArrayFormService } from '@app/shared/forms/form.service';
import { SecurityPermissionFacade } from '../permissions/permission.provider';
import { PermissionGroupVM } from './permission-group.vm';

export class AdministrationPermissionGroupFormService extends ArrayFormService<PermissionGroupVM> {
  private readonly _facades = {
    permissions: inject(SecurityPermissionFacade),
  };

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
            new MultiSelectFormFieldSchema<PermissionGroupVM>({
              key: 'permissions',
              label: 'Permissions',
              options: this._facades.permissions.data().map((e) => ({
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
