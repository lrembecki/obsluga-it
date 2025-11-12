import { Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Required } from 'app/core/directives/required';
import { Valid } from 'app/core/directives/valid';
import { Button } from 'app/shared/ui/button/button';
import { ButtonDelete } from 'app/shared/ui/button/button-delete';
import { ButtonReturn } from 'app/shared/ui/button/button-return';
import { ButtonSubmit } from 'app/shared/ui/button/button-submit';
import { CheckboxInputComponent } from "app/shared/ui/inputs/checkbox-input.component";
import { DateInputComponent, DateInputGreaterThanDirective, DateInputLowerThanDirective } from "app/shared/ui/inputs/date-input.component";
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { TextareaInputComponent } from 'app/shared/ui/inputs/textarea-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { TripFormAgenda } from "./trip-form.agenda";
import { TripFormHighlights } from "./trip-form.highlights";
import { TripFormImagesTs } from "./trip-form.images.ts";
import { TripContextModel, TripDTO, TripHighlightDTO } from './trip.dto';
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
    CheckboxInputComponent,
    DateInputLowerThanDirective,
    DateInputGreaterThanDirective,
    TripFormHighlights,
    TripFormImagesTs,
    TripFormAgenda
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

            @if (tripId()) {
              <app-button
                delete
                [facade]="{ identity: this.tripId(), facade: _services.trips }"
                (deleted)="returnToList()"
              />
            }
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
        <app-ui-panel>
          <app-text-input
            [value]="model.session().calendar || undefined"
            (valueChange)="model.session().calendar = $event ?? null"
            label="Calendar (optional)"
            [valid]="!model.session().calendar || model.session().calendar!.length <= 50"
          />

          <app-date-input 
            #schedulingStartDate
            [(value)]="model.session().startDate" 
            label="Start Date" 
            [lowerThan]="schedulingEndDate"
            (valueChange)="model.update()" />

          <app-date-input
            #schedulingEndDate
            [(value)]="model.session().endDate" 
            label="End Date" 
            [greaterThan]="schedulingStartDate"
            (valueChange)="model.update()" />
        </app-ui-panel>

        @if (model.session().calendar && (model.session().calendar?.length ?? 0) > 50) {
          <small class="error">Max 50 characters</small>
        }

        @if (!dateRangeValid()) {
          <div class="error">Start Date must be before or equal to End Date.</div>
        }

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
        
        <app-trip-form-highlights [(model)]="model" />
        <app-trip-form-images [(model)]="model" />
        <app-trip-form-agenda [(model)]="model" />

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
  protected readonly model = new TripContextModel(
    () => TripDTO.fromVM(
      this._services.trips.data().find(e => e.id === this.tripId()) ?? new TripVM(),
      this._services.highlights.data()
    ),
    entry => new TripDTO(entry)
  );

  protected readonly dateRangeValid = computed(() => {
    const s = this.model.session().startDate;
    const e = this.model.session().endDate;
    if (!s || !e) return true;
    return new Date(s) <= new Date(e);
  });

  ngOnInit(): void {
    if (!this.tripId()) {
      this.model.session().highlights = this._services.highlights.data().slice()
        .map((h, index) => TripHighlightDTO.create(index + 1, h));
      this.model.update();
    }
  }

  protected async submit(): Promise<void> {
    const session = this.model.session();

    if ((session.calendar?.length ?? 0) > 50) return;
    if (!this.dateRangeValid()) return;

    const response = this.tripId()
      ? await this._services.trips.update(this.tripId(), session)
      : await this._services.trips.create('', session);

    if (response.success) this.returnToList();
  }

  protected returnToList(): void {
    this._services.router.navigate(['/modules/trotamundos/trips']);
  }
}
