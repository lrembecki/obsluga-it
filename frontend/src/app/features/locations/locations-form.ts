import { Component, inject } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Button } from 'app/shared/ui/button/button';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { TextareaInputComponent } from 'app/shared/ui/inputs/textarea-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { LocationModel } from './locations.facade';
import {
  formGroupComputed,
  injectLocations,
  selectedModelComputed,
} from './locations.provider';

@Component({
  selector: 'app-locations-form',
  imports: [
    ReactiveFormsModule,
    UiPanel,
    Button,
    RouterLink,
    TextInputComponent,
    TextareaInputComponent,
    UiTable,
  ],
  template: `
    @if (form()) {
      <app-ui-panel>
        <ng-template #start>
          <app-button
            [disabled]="!form().valid"
            text="Submit"
            (buttonClick)="submit()"
            color="primary"
          />
        </ng-template>
        <ng-template #end>
          <app-button text="Return to list" [routerLink]="['../list']" />
        </ng-template>
      </app-ui-panel>

      <form [formGroup]="form()" class="mb-1">
        <app-text-input formControlName="name" label="Name" />
        <app-textarea-input formControlName="description" label="Description" />
      </form>

      <app-button upload />
      <app-ui-table [data]="model()!.images"> </app-ui-table>
    }
  `,
  styles: ``,
})
export class LocationsForm {
  private readonly services = injectLocations();
  private readonly router = inject(Router);
  protected readonly model = selectedModelComputed(this.services);
  protected readonly form = formGroupComputed(this.services);

  public async submit(): Promise<void> {
    const value = this.form().value as LocationModel;

    const response = await (value.locationId
      ? this.services.locations.update(
          value.locationId!.toString(),
          this.form().value,
        )
      : this.services.locations.create('', value));

    if (response.success) {
      this.router.navigate(['locations/list']);
    }
  }
}
