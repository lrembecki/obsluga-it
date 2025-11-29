import { Component } from '@angular/core';
import { isLoadingComputed } from 'app/core/helpers/facade.helper';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { DataTable } from 'app/shared/ui/data-table/data-table';
import { TableColumnConfig } from 'app/shared/ui/data-table/data-table.types';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectForms } from './forms.provider';
import { FormsModel } from './models/forms.model';

@Component({
  selector: 'app-forms-list',
  imports: [Button, UiPanel, DataTable, TranslatePipe],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button text="Add" color="primary" />
      </ng-template>
    </app-ui-panel>
    <app-data-table
      [data]="_facades.forms.data"
      [columns]="columns"
      [features]="{ quicksearch: true, sortable: true }"
      [persistenceKey]="'features-forms'"
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
export class FormsList {
  protected readonly _facades = injectForms();
  protected readonly loading = isLoadingComputed(this._facades);

  protected readonly columns: TableColumnConfig<FormsModel>[] = [
    { field: 'name', label: 'Name', type: 'text', sortable: true },
    { field: 'isActive', label: 'Is Active', type: 'text', sortable: true }
  ];

  protected onOrderBy(sort: { column: string; direction: 'asc' | 'desc' }) {
    this._facades.forms.filter({ sort });
  }

  protected onSearch(query: string) {
    this._facades.forms.filter({ q: query });
  }
}
