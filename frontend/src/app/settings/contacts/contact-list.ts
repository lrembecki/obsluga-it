import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { ContactModel } from './contact.model';
import { injectSettingContacts } from './contact.provider';

@Component({
  selector: 'app-contact-list',
  imports: [
    UiPanel,
    Button,
    UiTable,
    UiTableColumn,
    RouterLink,
    UiTableColumnLink,
  ],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-ui-table [data]="_services.contacts.data()">
      <app-ui-table-column
        text="Name"
        field="name"
        link
        [renderLink]="renderLink"
      />
      <app-ui-table-column text="Email" field="email" width="200px" />
      <app-ui-table-column text="Phone" field="phone" width="200px" />
      <app-ui-table-column text="Position" field="postion" width="250px" />
      <app-ui-table-column text="Is Active" field="isActive" width="120px" />
    </app-ui-table>
  `,
  styles: ``,
})
export class ContactList {
  protected readonly _services = injectSettingContacts();

  protected renderLink = (record: ContactModel) => ['..', record.contactId];
}
