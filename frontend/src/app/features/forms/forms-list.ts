import { Component } from '@angular/core';
import { isLoadingComputed } from 'app/core/helpers/facade.helper';
import { ListTemplate } from 'app/shared/templates/list-template';
import { Button } from 'app/shared/ui/button/button';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { injectForms } from './forms.provider';
import { FormsModel } from './models/forms.model';

@Component({
  selector: 'app-forms-list',
  imports: [Button, ListTemplate, UiTableColumn, UiTableColumnLink],
  template: `
    <app-list-template [data]="_facades.forms.data()">
      <app-button text="Add" panel-start #start />
      <app-ui-table-column
        text="Name"
        field="name"
        link
        [renderLink]="renderLink"
      />
      <app-ui-table-column text="Is Active" />
    </app-list-template>
  `,
  styles: ``,
})
export class FormsList {
  protected readonly _facades = injectForms();
  protected readonly loading = isLoadingComputed(this._facades);

  protected renderLink = (record: FormsModel) => ['..', record.formId];
}
