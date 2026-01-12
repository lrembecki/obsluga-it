import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema } from '@app/shared/forms';
import { ArrayFormService } from '@app/shared/forms/form.service';
import { PermissionVM } from './permission.vm';

export class AdministrationPermissionFormService extends ArrayFormService<PermissionVM> {
  constructor() {
    super();

    this._returnRoute.set(['/modules/administration/permissions/list']);

    effect(() => {
      this._schema.set(
        new FormSchema<PermissionVM>({
          fields: [
            new TextFormFieldSchema<PermissionVM>({
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
