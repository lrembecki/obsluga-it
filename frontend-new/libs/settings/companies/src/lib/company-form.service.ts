import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, GroupFormFieldSchema, CollectionFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { CompanyVM } from './company.vm';

export class SettingsCompanyFormService extends ArrayFormService<CompanyVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/settings/companies/list']);

    effect(() => {
      this._schema.set(
        new FormSchema<CompanyVM>({
          fields: [
            new TextFormFieldSchema<CompanyVM>({
              key: 'name',
              label: 'Name',
              validators: [Validators.required],
            }),
            new GroupFormFieldSchema<CompanyVM>({
              key: 'address' as any,
              label: 'Address',
              fields: [
                new TextFormFieldSchema({ key: 'address1', label: 'Address Line 1' }),
                new TextFormFieldSchema({ key: 'address2', label: 'Address Line 2' }),
                new TextFormFieldSchema({ key: 'city', label: 'City' }),
                new TextFormFieldSchema({ key: 'postalCode', label: 'Postal Code' }),
                new TextFormFieldSchema({ key: 'country', label: 'Country' }),
              ],
            }),
            new CollectionFormFieldSchema<CompanyVM>({
              key: 'parameters' as any,
              label: 'Parameters',
              itemFields: [
                new TextFormFieldSchema({ key: 'name', label: 'Name', validators: [Validators.required] }),
                new TextFormFieldSchema({ key: 'value', label: 'Value' }),
              ],
            }),
          ],
        }),
      );
    });
  }
}
