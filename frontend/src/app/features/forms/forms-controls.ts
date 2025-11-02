import { Component, computed } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ListTemplate } from 'app/shared/templates/list-template';
import { Button } from 'app/shared/ui/button/button';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { selectedFormComputed } from './forms.provider';
import { FormsModelElement } from './models/forms.model';

@Component({
  selector: 'app-forms-controls',
  imports: [ListTemplate, UiTableColumn, UiTableColumnLink, Button, RouterLink],
  template: `
    <app-list-template [data]="controls()">
      <app-button
        panel-end
        #end
        text="Return to list"
        [routerLink]="['../..']"
      />

      <app-ui-table-column text="Order" field="order" width="90px" />
      <app-ui-table-column
        text="Name"
        field="formControl.name"
        link=""
        [renderLink]="renderLink"
        width="350px"
      />
      <app-ui-table-column text="Type" field="formControl.type" width="100px" />
      <app-ui-table-column
        text="Required"
        field="formControl.required"
        width="90px"
      />
      <app-ui-table-column
        text="Default value"
        field="formControl.defaultValue"
        width="100px"
      />
    </app-list-template>
  `,
  styles: ``,
})
export class FormsControls {
  model = selectedFormComputed();
  controls = computed(() => this.model()?.controls ?? []);
  renderLink = (record: FormsModelElement) => [
    '..',
    'controls',
    record.formControlId,
  ];
}
