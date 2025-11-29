import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { DataTable } from 'app/shared/ui/data-table/data-table';
import { TableColumnConfig } from 'app/shared/ui/data-table/data-table.types';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectTrotamundosLoyalityPrograms } from './loyality-program.provider';
import { LoyalityProgramVM } from './loyality-program.vm';

@Component({
  selector: 'app-loyality-program-list',
  imports: [
    UiPanel,
    DataTable,
    RouterLink,
    Button,
    TranslatePipe
  ],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button color="primary" text="Create" routerLink="../create" />
      </ng-template>
    </app-ui-panel>
    <app-data-table
      [data]="_services.loyalityPrograms.data"
      [columns]="columns"
      [features]="{ quicksearch: true, sortable: true }"
      [persistenceKey]="'trotamundos-loyality-programs'"
      [searchPlaceholder]="'DATA_TABLE.SEARCH_PLACEHOLDER' | translate"
      [actionsLabel]="'DATA_TABLE.ACTIONS' | translate"
      [editLabel]="'DATA_TABLE.EDIT' | translate"
      [saveLabel]="'DATA_TABLE.SAVE' | translate"
      [cancelLabel]="'DATA_TABLE.CANCEL' | translate"
      (orderBy)="onOrderBy($event)"
      (searchQuery)="onSearch($event)"
    />
  `,
  styles: ``
})
export class LoyalityProgramList {
  protected readonly _services = injectTrotamundosLoyalityPrograms();

  protected readonly columns: TableColumnConfig<LoyalityProgramVM>[] = [
    { field: 'name', label: 'Name', type: 'text', width: '300px', sortable: true },
    { field: 'title', label: 'Title', type: 'text', width: '300px', sortable: true },
    { field: 'description', label: 'Description', type: 'text', sortable: true }
  ];

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._services.loyalityPrograms.filter({ sort });
  }

  protected onSearch(query: string) {
    this._services.loyalityPrograms.filter({ q: query });
  }
}
