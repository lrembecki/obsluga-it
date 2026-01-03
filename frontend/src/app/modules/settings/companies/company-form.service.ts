import { effect, inject } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  CollectionFormFieldSchema,
  FormSchema,
  GroupFormFieldSchema,
  MultiSelectFormFieldSchema,
  TextFormFieldSchema,
  TextareaFormFieldSchema,
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import { ContactsFacade } from '../contacts/contact.facade';
import { AddressVM, CompanyParameterVM, CompanyVM } from './company.vm';

export class SettingsCompanyFormService extends FormService<CompanyVM> {
  private readonly _facades = {
    contacts: inject(ContactsFacade),
  };

  constructor() {
    super();

    this._returnRoute.set(['/modules/settings/companies/']);

    effect(() => {
      this._schema.set(
        new FormSchema<CompanyVM>({
          patchValue: {
            parameters: [],
          },
          fields: [
            // Name: string
            new TextFormFieldSchema<CompanyVM>({
              label: 'Name',
              key: 'name',
              validators: [Validators.required],
              colClass: 'col-4',
            }),

            new MultiSelectFormFieldSchema<CompanyVM>({
              key: 'emailContact',
              label: 'Email Contacts',
              options: this._facades.contacts
                .data()
                .map((c) => ({ label: `${c.name} (${c.email})`, value: c.id })),
            }),

            new MultiSelectFormFieldSchema<CompanyVM>({
              key: 'phoneContact',
              label: 'Phone Contacts',
              options: this._facades.contacts
                .data()
                .map((c) => ({ label: `${c.name} (${c.phone})`, value: c.id })),
            }),

            // Address Section
            new GroupFormFieldSchema<CompanyVM, AddressVM>({
              key: 'address',
              label: 'Address',
              nestedFields: [
                new TextFormFieldSchema<AddressVM>({
                  label: 'Address Line 1',
                  key: 'address1',
                  validators: [Validators.required],
                  colClass: 'col-6',
                }),

                new TextFormFieldSchema<AddressVM>({
                  label: 'Address Line 2',
                  key: 'address2',
                  validators: [],
                  colClass: 'col-6',
                }),

                new TextFormFieldSchema<AddressVM>({
                  label: 'City',
                  key: 'city',
                  validators: [Validators.required],
                  colClass: 'col-4',
                }),

                new TextFormFieldSchema<AddressVM>({
                  label: 'Postal Code',
                  key: 'postalCode',
                  validators: [Validators.required],
                  colClass: 'col-4',
                }),

                new TextFormFieldSchema<AddressVM>({
                  label: 'Country',
                  key: 'country',
                  validators: [Validators.required],
                  colClass: 'col-4',
                }),
              ],
            }),

            // Parameters Collection
            new CollectionFormFieldSchema<CompanyVM, CompanyParameterVM>({
              label: 'Parameters',
              key: 'parameters',
              orderField: 'order',
              itemFields: [
                new TextFormFieldSchema<CompanyParameterVM>({
                  label: 'Order',
                  key: 'order',
                  isVisible: false,
                  validators: [Validators.required],
                  colClass: 'col-2',
                  disabled: true,
                }),

                new TextFormFieldSchema<CompanyParameterVM>({
                  label: 'Parameter Name',
                  key: 'name',
                  validators: [Validators.required],
                  colClass: 'col-5',
                }),

                new TextareaFormFieldSchema<CompanyParameterVM>({
                  label: 'Parameter Value',
                  key: 'value',
                  validators: [],
                  colClass: 'col-5',
                }),
              ],
              colClass: 'col-12',
            }),
          ],
        }),
      );
    });
  }
}
