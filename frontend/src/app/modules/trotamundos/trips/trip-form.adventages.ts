import { Component, computed, effect, model, signal } from '@angular/core';
import { MultiSelectInputComponent } from "@shared/ui/inputs/multi-select-input.component";
import { UiTable } from "@shared/ui/ui-table";
import { UiTableColumn } from "@shared/ui/ui-table-column";
import { AdvantageVM } from '../advantages/advantage.vm';
import { TripContextModel } from './trip.dto';
import { injectTrotamundosTrips } from './trip.provider';

@Component({
  selector: 'app-trip-form-adventages',
  imports: [MultiSelectInputComponent, UiTableColumn, UiTable],
  template: `
    <app-multi-select-input
      #input
      [data]="this._services.advantages.data()"
      textField="title"
      [(value)]="selected"
      (valueChange)="advantageListChange()"
    />

    <app-ui-table [data]="selected()" >
      <app-ui-table-column text="Title" width="200px" field="title" />
      <app-ui-table-column text="Content" field="content" />
    </app-ui-table>
  `,
  styles: `
    :host {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }
  `
})
export class TripFormAdventages {
  protected readonly _services = injectTrotamundosTrips();

  public readonly model = model.required<TripContextModel>();

  public readonly available = computed(() =>
    this._services
      .advantages
      .data()
      .filter((a) => !this.model().session().advantages.includes(a.id)),
  );
  public readonly selected = signal<AdvantageVM[]>([]);

  constructor() {
    effect(() => {
      this.selected.set(
        this._services
          .advantages
          .data()
          .filter((a) => this.model().session().advantages.includes(a.id)),
      );
    });
  }

  protected advantageListChange(): void {
    this.model().session().advantages = this.selected().map((a) => a.id);
    this.model().update();
  }
}
