import { Component, computed } from '@angular/core';
import { RouterLink } from '@angular/router';
import { provideFacadeData } from 'app/core/helpers/facade.helper';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { DataTable } from 'app/shared/ui/data-table/data-table';
import { TableColumnConfig } from 'app/shared/ui/data-table/data-table.types';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { LocationModel } from './locations.facade';
import { injectLocations } from './locations.provider';

@Component({
  selector: 'app-locations-list',
  imports: [
    DataTable,
    UiPanel,
    Button,
    RouterLink,
    TranslatePipe,
  ],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button text="Create" color="primary" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-data-table
      [data]="data"
      [columns]="columns"
      [features]="{ quicksearch: true, sortable: true }"
      [persistenceKey]="'features-locations'"
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
export class LocationsList {
  private readonly _services = injectLocations();
  protected readonly data = computed(
    () => provideFacadeData('locations', this._services)() as LocationModel[],
  );

  protected readonly columns: TableColumnConfig<LocationModel>[] = [
    { field: 'name', label: 'Name', type: 'text', width: '200px', sortable: true },
    { field: 'description', label: 'Description', type: 'text', width: '700px', sortable: true }
  ];

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._services.locations.filter({ sort });
  }

  protected onSearch(query: string) {
    this._services.locations.filter({ q: query });
  }
}
