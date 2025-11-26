import { Component, model } from '@angular/core';
import { Button } from "app/shared/ui/button/button";
import { TextInputComponent } from "app/shared/ui/inputs/text-input.component";
import { UiPanel } from "app/shared/ui/ui-panel";
import { TripContextModel } from './trip.dto';
import { injectTrotamundosTrips } from './trip.provider';

@Component({
  selector: 'app-trip-form-agenda',
  imports: [UiPanel, Button, TextInputComponent],
  template: `
      <app-ui-panel>
        <app-button text="Add Description" (buttonClick)="model().addAgenda()" />
      </app-ui-panel>

      @for (item of model().session().agenda; track item) {
        <div class="agenda-item">
          <app-text-input [(value)] ="item.content" (valueChange)="model().update()" />
          <app-button icon="pi pi-trash" (buttonClick)="model().removeAgenda(item.order!)" />
        </div>
      }
  `,
  styles: `
    :host {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }

    .agenda-item {
      display: flex;
      gap: .25rem;

      app-text-input {
        flex: auto;
      }
    }
  `
})
export class TripFormAgenda {
  protected readonly _services = injectTrotamundosTrips();
  public readonly model = model.required<TripContextModel>();
}
