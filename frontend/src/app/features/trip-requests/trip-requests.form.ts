import { Component, inject, model } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TripRequestFacade } from 'app/core/facades/trip-request.facade';
import { TripRequestModel } from 'app/core/models/trip-request.model';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { TranslationService } from 'app/core/services/translation.service';
import { ButtonPanelService } from '../../shared/ui/button-panel/button-panel';
import { Button } from '../../shared/ui/button/button';
import { DropdownInputComponent } from '../../shared/ui/inputs/dropdown-input.component';
import { NumberInputComponent } from '../../shared/ui/inputs/number-input.component';
import { TextInputComponent } from '../../shared/ui/inputs/text-input.component';

@Component({
  selector: 'app-trip-request-form',
  imports: [
    ReactiveFormsModule,
    TranslatePipe,
    TextInputComponent,
    DropdownInputComponent,
    NumberInputComponent,
    Button,
  ],
  template: `
    @if (_formGroup) {
      <h1>{{ 'Trip Request' | translate }}</h1>
      <div class="title">
        <app-button [text]="'Save' | translate" color="primary" />
        <app-button
          [text]="'Return' | translate"
          (buttonClick)="_router.navigate([_listPath])"
        />
      </div>

      <form [formGroup]="_formGroup">
        <!-- Editable status field -->
        <app-dropdown-input
          formControlName="status"
          [label]="'Status' | translate"
          [data]="statusOptions"
          textField="label"
        />

        <!-- Read-only fields displaying current values -->
        <app-text-input
          [label]="'Trip Request ID' | translate"
          [disabled]="true"
          [value]="_model()?.tripRequestId"
        />

        <app-text-input
          [label]="'Email' | translate"
          [disabled]="true"
          [value]="_model()?.email"
        />

        <app-text-input
          [label]="'First and Last Name' | translate"
          [disabled]="true"
          [value]="_model()?.firstAndLastName"
        />

        <app-text-input
          [label]="'Phone Number' | translate"
          [disabled]="true"
          [value]="_model()?.phoneNumber"
        />

        <app-text-input
          [label]="'Trip Direction' | translate"
          [disabled]="true"
          [value]="_model()?.tripDirection"
        />

        <app-text-input
          [label]="'Trip Date' | translate"
          [disabled]="true"
          [value]="_model()?.tripDate"
        />

        <app-text-input
          [label]="'Trip Duration' | translate"
          [disabled]="true"
          [value]="_model()?.tripDuration"
        />

        <app-number-input
          [label]="'Amount of People' | translate"
          [disabled]="true"
          [value]="_model()?.amountOfPeople"
        />

        <app-number-input
          [label]="'Amount of Kids' | translate"
          [disabled]="true"
          [value]="_model()?.amountOfKids"
        />

        <app-text-input
          [label]="'Group Relation Type' | translate"
          [disabled]="true"
          [value]="_model()?.groupRelationType"
        />

        <app-text-input
          [label]="'Preferred Airport' | translate"
          [disabled]="true"
          [value]="_model()?.preferedAirport"
        />

        <app-text-input
          [label]="'Preferred Trip Type' | translate"
          [disabled]="true"
          [value]="_model()?.preferedTripType"
        />
      </form>
    }
  `,
  host: {
    class: 'trip-request-form',
  },
  styles: `
    ::ng-deep .trip-request-form,
    ::ng-deep .trip-request-form form {
      display: flex;
      flex-direction: column;
      gap: 2rem;
    }

    ::ng-deep .trip-request-form input,
    ::ng-deep .trip-request-form .p-select {
      width: 100%;
    }

    .title {
      display: flex;
      justify-content: space-between;
    }
  `,
  providers: [ButtonPanelService],
})
export class TripRequestForm {
  protected readonly _listPath = `trotamundos/trip-requests`;

  private readonly _formBuilder = inject(FormBuilder);
  private readonly _route = inject(ActivatedRoute);
  protected readonly _router = inject(Router);
  private readonly _buttonPanel = inject(ButtonPanelService);
  private readonly _translations = inject(TranslationService);
  private readonly _facade = inject(TripRequestFacade);

  protected _formGroup: FormGroup = null!;
  protected _model = model<TripRequestModel>();
  protected statusOptions = [
    { value: 'New', label: this._translations.instant('UI.NEW') },
  ];

  ngOnInit(): void {
    this._buttonPanel.data.set([
      {
        label: this._translations.instant('UI.SAVE'),
        command: async (event) => {
          if (!this._formGroup.valid) return;
          event.item!.disabled = true;

          const value = this._formGroup.value;
          const response = await this._facade.update(
            `/${this._model()!.tripRequestId}`,
            value,
          );

          event.item!.disabled = false;

          if (response.success) {
            this._router.navigate(['trotamundos/files']);
          }
        },
      },
    ]);

    this._route.data.subscribe((data) => {
      this._model.set(data['model']);
      this._formGroup = this.createFormGroup(data['model']);
    });
  }

  private createFormGroup(model: TripRequestModel): FormGroup {
    return this._formBuilder.group({
      status: new FormControl(model.status, [Validators.required]),
    });
  }
}
