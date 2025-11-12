import { Component, computed, model } from '@angular/core';
import { Button } from "app/shared/ui/button/button";
import { DropdownInputComponent } from "app/shared/ui/inputs/dropdown-input.component";
import { TextInputComponent } from "app/shared/ui/inputs/text-input.component";
import { UiPanel } from "app/shared/ui/ui-panel";
import { UiTable } from "app/shared/ui/ui-table";
import { UiTableColumn } from "app/shared/ui/ui-table-column";
import { TripContextModel } from './trip.dto';
import { injectTrotamundosTrips } from './trip.provider';

@Component({
  selector: 'app-trip-form-highlights',
  imports: [UiPanel, DropdownInputComponent, Button, UiTable, UiTableColumn, TextInputComponent],
  template: `
      <h2>Highlight</h2>
      @if (availableHighlights().length) {
        <app-ui-panel>
          <app-dropdown-input #selectedHighlightInput [data]="availableHighlights()"
            textField="title" />
          <app-button text="Add Description" (buttonClick)="model().addHighlight(selectedHighlightInput.value())" />
        </app-ui-panel>
      }
      <app-ui-table [data]="model().session().highlights">
        <app-ui-table-column text="Content" field="value" header="Value">
          <ng-template #cell let-entry="record">
            <app-text-input [(value)] ="entry.value" (valueChange)="model().update()" />
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
export class TripFormHighlights {
  protected readonly _services = injectTrotamundosTrips();
  protected readonly availableHighlights = computed(() =>
    this._services.highlights.data().filter(h =>
      !this.model().session().highlights.some(th => th.highlightId === h.id)
    )
  );
  public readonly model = model.required<TripContextModel>();
}
