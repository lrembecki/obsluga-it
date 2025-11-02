import { Component, contentChildren, input } from '@angular/core';
import { UiPanel } from '../ui/ui-panel';
import { UiTable } from '../ui/ui-table';
import { UiTableColumn } from '../ui/ui-table-column';

@Component({
  selector: 'app-list-template',
  imports: [UiPanel, UiTable],
  template: `
    @if (panelStartElements().length || panelEndElements().length) {
      <app-ui-panel>
        @if (panelStartElements().length) {
          <ng-template #start>
            <ng-content select="[panel-start]" />
          </ng-template>
        }
        @if (panelEndElements().length) {
          <ng-template #end>
            <ng-content select="[panel-end]" />
          </ng-template>
        }
      </app-ui-panel>
    }

    <app-ui-table [columnsInput]="columns()" [data]="data()">
      <ng-content select="app-ui-table-column" />
    </app-ui-table>
  `,
  styles: `
    :host {
      position: relative;
    }
  `,
})
export class ListTemplate {
  public readonly data = input<any[]>([]);
  protected readonly columns = contentChildren<UiTableColumn>(UiTableColumn);
  protected readonly panelStartElements = contentChildren('start');
  protected readonly panelEndElements = contentChildren('end');

  constructor() {}
}
