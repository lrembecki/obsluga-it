import { effect, inject } from '@angular/core';
import { Validators } from '@angular/forms';
import {
  FormSchema,
  GroupFormFieldSchema,
  ImageFormFieldSchema,
  SelectFormFieldSchema,
  TextareaFormFieldSchema,
  TextFormFieldSchema,
} from '@app/shared/forms';
import { FormService } from '@app/shared/forms/form.service';
import { SettingsCompanyFacade } from '../companies/company.facade';
import { WebsiteMetaVM, WebsiteVM } from './website.vm';

export class SettingsWebsiteFormService extends FormService<WebsiteVM> {
  private readonly _facades = {
    companies: inject(SettingsCompanyFacade),
  };

  constructor() {
    super();

    this._returnRoute.set(['/modules/settings/websites/']);

    effect(() => {
      const companyOptions = this._facades.companies.data().map((company) => ({
        label: company.name,
        value: company.id,
      }));

      this._schema.set(
        new FormSchema<WebsiteVM>({
          patchValue: {
            meta: new WebsiteMetaVM(),
          },
          fields: [
            new TextFormFieldSchema<WebsiteVM>({
              label: 'Title',
              key: 'title',
              validators: [Validators.required],
              colClass: 'col-6',
            }),
            new TextFormFieldSchema<WebsiteVM>({
              label: 'Url',
              key: 'url',
              validators: [Validators.required],
              colClass: 'col-6',
            }),
            new SelectFormFieldSchema<WebsiteVM>({
              label: 'Company',
              key: 'companyId',
              placeholder: 'Select company',
              validators: [Validators.required],
              options: companyOptions,
              colClass: 'col-6',
            }),
            new GroupFormFieldSchema<WebsiteVM, WebsiteMetaVM>({
              key: 'meta',
              label: 'Meta',
              nestedFields: [
                new TextFormFieldSchema<WebsiteMetaVM>({
                  label: 'Meta Title',
                  key: 'title',
                  validators: [Validators.required],
                  colClass: 'col-6',
                }),
                new TextFormFieldSchema<WebsiteMetaVM>({
                  label: 'Keywords',
                  key: 'keywords',
                  colClass: 'col-6',
                }),
                new TextareaFormFieldSchema<WebsiteMetaVM>({
                  label: 'Description',
                  key: 'description',
                  rows: 5,
                  colClass: 'col-12',
                }),
                new ImageFormFieldSchema<WebsiteMetaVM>({
                  key: 'image' as any,
                  label: 'Image',
                  colClass: 'col-10',
                  validators: [Validators.required],
                  editable: true,
                }),
                new TextFormFieldSchema<WebsiteMetaVM>({
                  key: 'imageId' as any,
                  disabled: true,
                  isVisible: false,
                  colClass: 'col-2',
                }),
              ],
            }),
          ],
        }),
      );
    });
  }
}
