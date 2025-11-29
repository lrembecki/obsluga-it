import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { DataTable } from 'app/shared/ui/data-table/data-table';
import { TableColumnConfig } from 'app/shared/ui/data-table/data-table.types';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectTrotamundosAdvantages } from './advantage.provider';
import { AdvantageVM } from './advantage.vm';

@Component({
  selector: 'app-advantage-list',
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
      [data]="_services.advantages.data"
      [columns]="columns"
      [features]="{ quicksearch: true, sortable: true }"
      [persistenceKey]="'trotamundos-advantages'"
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
export class AdvantageList {
  protected readonly _services = injectTrotamundosAdvantages();

  protected readonly columns: TableColumnConfig<AdvantageVM>[] = [
    { field: 'title', label: 'Title', type: 'text', width: '200px', sortable: true, renderLink: (record) => ['..', record.id] },
    { field: 'content', label: 'Content', type: 'text', sortable: true }
  ];

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._services.advantages.filter({ sort });
  }

  protected onSearch(query: string) {
    this._services.advantages.filter({ query });
  }
}
