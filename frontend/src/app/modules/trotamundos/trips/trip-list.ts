import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TableTemplate } from "app/shared/templates/table/table-template";
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectTrotamundosTrips } from './trip.provider';

@Component({
  selector: 'app-trip-list',
  imports: [
    UiPanel,
    RouterLink,
    Button,
    TableTemplate
  ],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-table-template [columns]="_services.columns.data()" [data]="_services.trips.data()" />
  `,
  styles: ``
})
export class TripList {
  protected readonly _services = injectTrotamundosTrips();
}
