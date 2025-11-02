import { Component, computed } from '@angular/core';
import { RouterLink } from '@angular/router';
import { provideFacadeData } from 'app/core/helpers/facade.helper';
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { UiTableColumnLink } from 'app/shared/ui/ui-table-column-link';
import { LocationModel } from './locations.facade';
import { injectLocations } from './locations.provider';

@Component({
  selector: 'app-locations-list',
  imports: [
    UiTable,
    UiTableColumn,
    UiTableColumnLink,
    UiPanel,
    Button,
    RouterLink,
  ],
  template: `
    <app-ui-panel>
      <app-button text="Create" color="primary" routerLink="../create" />
    </app-ui-panel>
    <app-ui-table [data]="data()">
      <app-ui-table-column
        link
        [renderLink]="renderLink"
        text="Name"
        field="name"
        width="200px"
      />
      <app-ui-table-column
        text="Description"
        field="description"
        width="700px"
      />
    </app-ui-table>
  `,
  styles: ``,
})
export class LocationsList {
  private readonly _services = injectLocations();
  protected readonly data = computed(
    () => provideFacadeData('locations', this._services)() as LocationModel[],
  );
  protected renderLink = (record: LocationModel) => [
    '/locations',
    record.locationId,
  ];
}
