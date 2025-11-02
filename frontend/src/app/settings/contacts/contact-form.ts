import { ChangeDetectionStrategy, Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Required } from 'app/core/directives/required';
import { Valid } from 'app/core/directives/valid';
import { Button } from 'app/shared/ui/button/button';
import { ButtonDelete } from 'app/shared/ui/button/button-delete';
import { ButtonReturn } from 'app/shared/ui/button/button-return';
import { ButtonSubmit } from 'app/shared/ui/button/button-submit';
import { CheckboxInputComponent } from 'app/shared/ui/inputs/checkbox-input.component';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { TranslatePipe } from '../../core/pipes/translate.pipe';
import { ContactModel } from './contact.model';
import { injectSettingContacts } from './contact.provider';

@Component({
  selector: 'app-contact-form',
  changeDetection: ChangeDetectionStrategy.Default,
  hostDirectives: [
    {
      directive: Valid,
      outputs: ['isValidChange'],
    },
  ],
  imports: [
    UiPanel,
    Button,
    RouterLink,
    TranslatePipe,
    ReactiveFormsModule,
    TextInputComponent,
    CheckboxInputComponent,
    Valid,
    Required,
    ButtonSubmit,
    ButtonReturn,
    ButtonDelete,
  ],
  template: `
    @if (model()) {
      <ng-container validate #validate="validate">
        <h1>{{ 'Contacts' | translate }}</h1>
        <app-ui-panel>
          <ng-template #start>
            <app-button
              submit
              [disabled]="!validate.isValid()"
              (buttonClick)="submit()"
              [isInProgress]="_services.contacts.saving()"
            />
          </ng-template>
          <ng-template #end>
            <app-button routerLink="../list" return />
          </ng-template>
        </app-ui-panel>

        <app-checkbox-input [(value)]="model()!.isActive" label="Active" />
        <app-text-input
          [disabled]="!!model().contactId"
          [(value)]="model().name"
          [required]="true"
          label="Name"
        />
        <app-text-input
          [(value)]="model().email"
          [required]="true"
          label="Email"
          type="email"
        />
        <app-text-input
          [(value)]="model().phone"
          [required]="true"
          label="Phone"
        />
        <app-text-input [(value)]="model().position" label="Position" />

        @if (model().contactId) {
          <app-ui-panel>
            <ng-template #end>
              <app-button
                delete
                [facade]="{
                  identity: model().contactId,
                  facade: _services.contacts,
                }"
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
  `,
})
export class ContactForm {
  protected readonly _services = injectSettingContacts();
  protected readonly routeParams = toSignal(
    this._services.activatedRoute.params,
  );
  protected readonly contactId = computed(
    () => this.routeParams()!['contactId'] as string,
  );
  protected readonly model = computed(
    () =>
      this._services.contacts
        .data()
        .find((e) => e.contactId === this.contactId()) ?? new ContactModel(),
  );

  protected minContactNumber() {
    return (
      Math.max(...this._services.contacts.data().map((e) => e.order), 0) + 1
    );
  }

  protected async submit(): Promise<void> {
    const model = this.model();

    model.order =
      Math.max(...this._services.contacts.data().map((e) => e.order), 0) + 1;

    const response = model.contactId
      ? await this._services.contacts.update(model.contactId, model)
      : await this._services.contacts.create('', model);

    if (response.success) {
      this.returnToList();
    }
  }

  protected returnToList() {
    this._services.router.navigate(['/settings/contacts/list']);
  }
}
