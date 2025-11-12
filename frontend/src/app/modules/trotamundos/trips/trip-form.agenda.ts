import { Component, model } from '@angular/core';
import { Button } from "app/shared/ui/button/button";
import { TextInputComponent } from "app/shared/ui/inputs/text-input.component";
import { UiPanel } from "app/shared/ui/ui-panel";
import { UiTable } from "app/shared/ui/ui-table";
import { UiTableColumn } from "app/shared/ui/ui-table-column";
import { TripContextModel } from './trip.dto';
import { injectTrotamundosTrips } from './trip.provider';

@Component({
  selector: 'app-trip-form-agenda',
  imports: [UiPanel, Button, UiTable, UiTableColumn, TextInputComponent],
  template: `
      <h2>Highlight</h2>
      <app-ui-panel>
          <app-button text="Add Description" (buttonClick)="model().addAgenda()" />
        </app-ui-panel>
      <app-ui-table [data]="model().session().agenda">
        <app-ui-table-column text="Content" field="content">
          <ng-template #cell let-entry="record">
            <app-text-input [(value)] ="entry.content" (valueChange)="model().update()" />
          </ng-template>
        </app-ui-table-column>
        <app-ui-table-column width="90px">
          <ng-template #cell let-entry="record">
            <app-button icon="pi pi-trash" (buttonClick)="model().removeHighlight(entry.highlightId)" />
          </ng-template>
        </app-ui-table-column>
      </app-ui-table>
  `,
  styles: ``
})
export class TripFormAgenda {
  protected readonly _services = injectTrotamundosTrips();
  public readonly model = model.required<TripContextModel>();
}
