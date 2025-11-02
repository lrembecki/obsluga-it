import { Component, model } from '@angular/core';
import { ListTemplate } from 'app/shared/templates/list-template';
import { MultiSelectInputComponent } from 'app/shared/ui/inputs/multi-select-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnDate } from 'app/shared/ui/ui-table-column-date';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { ContactRequestModel } from './contact-request.model';
import { injectServices } from './contact-request.provider';

@Component({
  selector: 'app-contact-request',
  imports: [
    ListTemplate,
    UiTableColumn,
    UiTableColumnLink,
    UiTableColumnDate,
    UiPanel,
    MultiSelectInputComponent,
  ],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-multi-select-input
          [data]="['new', 'active', 'abandoned', 'closed']"
          [(value)]="status"
          (valueChange)="status.set($event); filterChange()"
        />
      </ng-template>
    </app-ui-panel>
    <app-list-template [data]="data()">
      <app-ui-table-column
        width="300px"
        field="contactRequestId"
        link
        text="Identifier"
        [renderLink]="renderLink"
      />
      <app-ui-table-column field="dateTime" date width="170px" />
      <app-ui-table-column text="Email" field="email" width="200px" />
      <app-ui-table-column
        text="Full name"
        field="imieNazwisko"
        width="200px"
      />
      <app-ui-table-column text="Status" field="status" width="100px">
        <ng-template #filter>
          <app-multi-select-input
            [data]="['new', 'active', 'abandoned', 'closed']"
            [(value)]="status"
            (valueChange)="status.set($event); filterChange()"
          />
        </ng-template>
      </app-ui-table-column>
    </app-list-template>
  `,
  styles: `
    :host {
      width: 100%;
    }
  `,
})
export class ContactRequest {
  protected readonly services = injectServices();
  protected readonly status = model<string[]>(['new', 'active']);

  protected data = this.services.contacts.data;

  protected renderLink = (record: ContactRequestModel) => [
    '..',
    record.contactRequestId,
  ];

  protected filterChange() {
    this.services.contacts.filter({
      status: this.status(),
    });
  }
}
