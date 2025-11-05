import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { injectTrotamundosTrips } from './trip.provider';
import { TripVM } from './trip.vm';

@Component({
  selector: 'app-trip-list',
  imports: [
    UiPanel,
    UiTable,
    UiTableColumn,
    RouterLink,
    UiTableColumnLink,
    Button
  ],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-ui-table [data]="_services.trips.data()">
      <app-ui-table-column
        text="Name"
        field="name" width="180px"
        link
        [renderLink]="renderLink"
      />
      <app-ui-table-column
        text="Title"
        field="title" 
      />
      <app-ui-table-column text="Subtitle" field="subtitle" />
      <app-ui-table-column text="Active" field="isActive" width="100px" />
      <app-ui-table-column text="Disabled" field="isDisabled" width="110px" />
    </app-ui-table>
  `,
  styles: ``
})
export class TripList {
  protected readonly _services = injectTrotamundosTrips();
  protected renderLink = (record: TripVM) => ['..', record.id];
}
