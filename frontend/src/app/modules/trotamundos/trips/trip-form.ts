import { Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Required } from 'app/core/directives/required';
import { Valid } from 'app/core/directives/valid';
import { cachedComputed } from 'app/core/helpers/signal.helper';
import { Button } from 'app/shared/ui/button/button';
import { ButtonDelete } from 'app/shared/ui/button/button-delete';
import { ButtonReturn } from 'app/shared/ui/button/button-return';
import { ButtonSubmit } from 'app/shared/ui/button/button-submit';
import { CheckboxInputComponent } from "app/shared/ui/inputs/checkbox-input.component";
import { DateInputComponent } from "app/shared/ui/inputs/date-input.component";
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { TextareaInputComponent } from 'app/shared/ui/inputs/textarea-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { TripDTO } from './trip.dto';
import { injectTrotamundosTrips } from './trip.provider';
import { TripVM } from './trip.vm';

@Component({
  selector: 'app-trip-form',
  hostDirectives: [
    { directive: Valid, outputs: ['isValidChange'] },
  ],
  imports: [
    UiPanel,
    Button,
    RouterLink,
    ReactiveFormsModule,
    FormsModule,
    TextInputComponent,
    TextareaInputComponent,
    Valid,
    Required,
    ButtonSubmit,
    ButtonReturn,
    ButtonDelete,
    DateInputComponent,
    CheckboxInputComponent
],
  template: `
    @if (model.session()) {
      <ng-container validate #validate="validate">
        <h1>Trips</h1>
        <app-ui-panel>
          <ng-template #start>
            <app-button
              submit
              [disabled]="!validate.isValid() || !dateRangeValid()"
              (buttonClick)="submit()"
              [isInProgress]="_services.trips.saving()"
            />
          </ng-template>
          <ng-template #end>
            <app-button routerLink="../list" return />
          </ng-template>
        </app-ui-panel>

        <app-text-input
          [(value)]="model.session().name"
          [required]="true"
          label="Name"
          (valueChange)="model.update()"
          maxlength="250"
        />

        <app-ui-panel>
          <ng-template #start>
            <app-checkbox-input [(value)]="model.session().isActive" label="Active" />
            <app-checkbox-input [(value)]="model.session().isDisabled" label="Disabled" />
          </ng-template>
        </app-ui-panel>

        <!-- Scheduling fields -->
        <div class="scheduling">
          <app-ui-panel>
            <ng-template #start>
              <app-text-input
                [value]="model.session().calendar || undefined"
                (valueChange)="model.session().calendar = $event ?? null"
                label="Calendar (optional)"
                [valid]="!model.session().calendar || model.session().calendar!.length <= 50"
              />

              <app-date-input 
                [(value)]="model.session().startDate" 
                label="Start Date" 
                [valid]="!model.session().startDate == null || (!!model.session().endDate && +model.session().startDate <= +model.session().endDate)"
                (valueChange)="model.update()" />

              <app-date-input 
                [(value)]="model.session().endDate" 
                label="End Date" 
                [valid]="!model.session().endDate == null || (!!model.session().startDate && +model.session().endDate >= +model.session().startDate)"
                (valueChange)="model.update()" />
            </ng-template>
          </app-ui-panel>
          @if (model.session().calendar && (model.session().calendar?.length ?? 0) > 50) {
            <small class="error">Max 50 characters</small>
          }
        @if (!dateRangeValid()) {
          <div class="error">Start Date must be before or equal to End Date.</div>
        }
        </div>

        <app-text-input
          [(value)]="model.session().title"
          [required]="true"
          (valueChange)="model.update()"
          label="Title"
        />
        <app-text-input
          [(value)]="model.session().subtitle"
          [required]="true"
          label="Subtitle"
          (valueChange)="model.update()"
        />
        <app-textarea-input
          [(value)]="model.session().description"
          [required]="true"
          label="Description"
          (valueChange)="model.update()"
        />

        @if (tripId()) {
          <app-ui-panel>
            <ng-template #end>
              <app-button
                delete
                [facade]="{ identity: this.tripId(), facade: _services.trips }"
                (deleted)="returnToList()"
              />
            </ng-template>
          </app-ui-panel>
        }
      </ng-container>
    }
  `,
  styles: `
    :host {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }
  `
})
export class TripForm {
  protected readonly _services = injectTrotamundosTrips();
  protected readonly routeParams = toSignal(this._services.activatedRoute.params);
  protected readonly tripId = computed(() => this.routeParams()?.['id'] as string);
  protected readonly model = cachedComputed(
    () => TripDTO.fromVM(this._services.trips.data().find(e => e.id === this.tripId()) ?? new TripVM()),
    entry => new TripDTO(entry)
  );

  protected readonly dateRangeValid = computed(() => {
    const s = this.model.session().startDate;
    const e = this.model.session().endDate;
    if (!s || !e) return true;
    return new Date(s) <= new Date(e);
  });

  protected async submit(): Promise<void> {
    const session = this.model.session();

    if ((session.calendar?.length ?? 0) > 50) return;
    if (!this.dateRangeValid()) return;

    const response = this.tripId()
      ? await this._services.trips.update(this.tripId(), session)
      : await this._services.trips.create('', session);

    if (response.success) this.returnToList();
  }

  protected returnToList() {
    this._services.router.navigate(['/modules/trotamundos/trips']);
  }
}
