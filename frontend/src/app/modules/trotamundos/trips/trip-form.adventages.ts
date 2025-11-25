import { Component, model } from '@angular/core';
import { MultiSelectInputComponent } from "app/shared/ui/inputs/multi-select-input.component";
import { TripContextModel } from './trip.dto';
import { injectTrotamundosTrips } from './trip.provider';

@Component({
  selector: 'app-trip-form-adventages',
  imports: [MultiSelectInputComponent],
  template: `
    <app-multi-select-input [data]="_services.advantages.data()" textField="title" />
  `,
  styles: ``
})
export class TripFormAdventages {
  protected readonly _services = injectTrotamundosTrips();
  public readonly model = model.required<TripContextModel>();
}
