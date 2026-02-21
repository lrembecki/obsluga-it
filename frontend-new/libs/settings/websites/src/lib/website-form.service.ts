import { effect } from '@angular/core';
import { Validators } from '@angular/forms';
import { FormSchema, TextFormFieldSchema, ArrayFormService } from '@obsluga-it/shared/forms';
import { WebsiteVM } from './website.vm';

export class SettingsWebsiteFormService extends ArrayFormService<WebsiteVM> {
  constructor() {
    super();
    this._returnRoute.set(['/modules/settings/websites/list']);
    effect(() => {
      this._schema.set(new FormSchema<WebsiteVM>({
        fields: [
          new TextFormFieldSchema<WebsiteVM>({ key: 'name', label: 'Name', validators: [Validators.required] }),
          new TextFormFieldSchema<WebsiteVM>({ key: 'url', label: 'URL', validators: [Validators.required] }),
        ],
      }));
    });
  }
}
