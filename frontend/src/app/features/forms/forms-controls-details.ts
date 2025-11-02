import { JsonPipe } from '@angular/common';
import { Component, computed, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { DropdownInputComponent } from 'app/shared/ui/inputs/dropdown-input.component';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { selectedFormControlComputed } from './forms.provider';

@Component({
  selector: 'app-forms-controls-details',
  imports: [
    JsonPipe,
    TranslatePipe,
    UiPanel,
    Button,
    RouterLink,
    ReactiveFormsModule,
    TextInputComponent,
    DropdownInputComponent,
  ],
  template: `
    @if (model()) {
      <h1>
        {{ 'Form Control' | translate }}: {{ model()?.formControl?.name }}
      </h1>

      <app-ui-panel>
        <ng-template #start>
          <app-button text="Submit" color="primary" />
        </ng-template>
        <ng-template #end>
          <app-button text="Return to list" [routerLink]="['..']" />
        </ng-template>
      </app-ui-panel>

      <form [formGroup]="formGroup()!">
        <app-text-input formControlName="name" label="Name" />
        <app-dropdown-input
          formControlName="type"
          [data]="[
            'text',
            'date-range',
            'number',
            'date[]',
            'select',
            'boolean',
            'multiselect',
            'textarea',
          ]"
          [textField]="undefined"
          label="Type"
        />
      </form>

      {{ formGroup()!.value | json }}
    }
  `,
  styles: ``,
})
export class FormsControlsDetails {
  private readonly _formBuilder = inject(FormBuilder);
  model = selectedFormControlComputed();
  formGroup = computed(() =>
    !this.model()
      ? null
      : this._formBuilder.group({
          formControlId: [this.model()!.formControlId, [Validators.required]],
          name: [this.model()?.formControl.name, [Validators.required]],
          type: [this.model()?.formControl.type, [Validators.required]],
        }),
  );
}
