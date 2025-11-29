import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { DataTable } from 'app/shared/ui/data-table/data-table';
import { TableColumnConfig } from 'app/shared/ui/data-table/data-table.types';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { ContactModel } from './contact.model';
import { injectSettingContacts } from './contact.provider';

@Component({
  selector: 'app-contact-list',
  imports: [
    UiPanel,
    Button,
    DataTable,
    RouterLink,
    TranslatePipe,
  ],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-data-table
      [data]="_services.contacts.data"
      [columns]="columns"
      [features]="{ quicksearch: true, sortable: true }"
      [persistenceKey]="'contacts'"
      [searchPlaceholder]="'DATA_TABLE.SEARCH_PLACEHOLDER' | translate"
      [actionsLabel]="'DATA_TABLE.ACTIONS' | translate"
      [editLabel]="'DATA_TABLE.EDIT' | translate"
      [saveLabel]="'DATA_TABLE.SAVE' | translate"
      [cancelLabel]="'DATA_TABLE.CANCEL' | translate"
      (orderBy)="onOrderBy($event)"
      (searchQuery)="onSearch($event)"
    />
  `,
  styles: ``,
})
export class ContactList {
  protected readonly _services = injectSettingContacts();

  protected readonly columns: TableColumnConfig<ContactModel>[] = [
    { field: 'name', label: 'Name', type: 'text', sortable: true },
    { field: 'email', label: 'Email', type: 'text', width: '200px', sortable: true },
    { field: 'phone', label: 'Phone', type: 'text', width: '200px', sortable: true },
    { field: 'position', label: 'Position', type: 'text', width: '250px', sortable: true },
    { field: 'isActive', label: 'Is Active', type: 'text', width: '120px', sortable: true }
  ];

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._services.contacts.filter({ sort });
  }

  protected onSearch(query: string) {
    this._services.contacts.filter({ q: query });
  }
}