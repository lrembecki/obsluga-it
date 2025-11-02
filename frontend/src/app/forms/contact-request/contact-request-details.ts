import { Component, computed, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Button } from 'app/shared/ui/button/button';
import { DateInputComponent } from 'app/shared/ui/inputs/date-input.component';
import { DropdownInputComponent } from 'app/shared/ui/inputs/dropdown-input.component';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { TextareaInputComponent } from 'app/shared/ui/inputs/textarea-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import {
  injectServices,
  selectedModelComputed,
} from './contact-request.provider';

@Component({
  selector: 'app-contact-request-details',
  imports: [
    ReactiveFormsModule,
    UiPanel,
    Button,
    TextInputComponent,
    DateInputComponent,
    TextareaInputComponent,
    DropdownInputComponent,
  ],
  template: `
    @if (formGroup()) {
      <app-ui-panel>
        <ng-template #start>
          <app-button
            text="Submit"
            [disabled]="formGroup()!.invalid"
            color="primary"
            [isInProgress]="services.contacts.saving()"
            (buttonClick)="submit()"
          />
        </ng-template>
        <ng-template #end> </ng-template>
      </app-ui-panel>
      <form [formGroup]="formGroup()!">
        <div style="display: flex; gap: 1rem;">
          <app-dropdown-input
            label="Status"
            formControlName="status"
            [data]="['new', 'active', 'abandoned', 'closed']"
          />
          <app-date-input
            formControlName="dateTime"
            label="Date/Time"
            [disabled]="true"
          />
        </div>

        <app-text-input
          formControlName="email"
          label="Email"
          [disabled]="true"
        />
        <app-text-input
          formControlName="imieNazwisko"
          label="Full name"
          [disabled]="true"
        />
        <app-textarea-input
          formControlName="wiadomosc"
          label="Message"
          [disabled]="true"
        />
      </form>
    }
  `,
  styles: ``,
})
export class ContactRequestDetails {
  services = injectServices();
  model = selectedModelComputed(this.services);
  formBuilder = inject(FormBuilder);
  formGroup = computed(() =>
    !this.model()
      ? null
      : this.formBuilder.group({
          dateTime: [this.model()?.dateTime, [Validators.required]],
          email: [this.model()?.email, [Validators.required]],
          imieNazwisko: [this.model()?.imieNazwisko, [Validators.required]],
          wiadomosc: [this.model()?.wiadomosc, [Validators.required]],
          status: [this.model()?.status, [Validators.required]],
        }),
  );

  async submit(): Promise<void> {
    const response = await this.services.contacts.update(
      this.model()!.contactRequestId,
      {
        status: this.formGroup()?.value.status,
      },
    );

    if (response.success) {
      this.services.router.navigate(['/forms/contact-requests/']);
    }
  }
}
