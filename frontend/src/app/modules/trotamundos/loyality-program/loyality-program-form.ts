import { Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { Required } from 'app/core/directives/required';
import { Valid } from 'app/core/directives/valid';
import { cachedComputed } from 'app/core/helpers/signal.helper';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { UploadImageInput } from "app/shared/storage/upload-image-input";
import { Button } from 'app/shared/ui/button/button';
import { ButtonDelete } from 'app/shared/ui/button/button-delete';
import { ButtonReturn } from 'app/shared/ui/button/button-return';
import { ButtonSubmit } from 'app/shared/ui/button/button-submit';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { TextareaInputComponent } from 'app/shared/ui/inputs/textarea-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectTrotamundosLoyalityPrograms } from './loyality-program.provider';
import { LoyalityProgramVM } from './loyality-program.vm';

@Component({
  selector: 'app-loyality-program-form',
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
    TextInputComponent,
    TextareaInputComponent,
    Valid,
    Required,
    ButtonSubmit,
    ButtonReturn,
    ButtonDelete,
    UploadImageInput
],
  template: `
    @if (model.session()) {
      <ng-container validate #validate="validate">
        <h1>{{ 'Loyality Program' | translate }}</h1>
        <app-ui-panel>
          <ng-template #start>
            <app-button
              submit
              [disabled]="!validate.isValid()"
              (buttonClick)="submit()"
              [isInProgress]="_services.loyalityPrograms.saving()"
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
        />

        <app-text-input
          [disabled]="!!model.session().id"
          [(value)]="model.session().title"
          [required]="true"
          label="Title"
        />

        <app-textarea-input
          [(value)]="model.session().description"
          [required]="true"
          label="Description"
        />

  <app-upload-image-input [required]="true" [(value)]="model.session().image" />

  @if (model.session().id) {
          <app-ui-panel>
            <ng-template #end>
              <app-button
                delete
                [facade]="{
                  identity: model.session().id,
                  facade: _services.loyalityPrograms,
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
  `
})
export class LoyalityProgramForm {
  protected readonly _services = injectTrotamundosLoyalityPrograms();
  protected readonly routeParams = toSignal(
    this._services.activatedRoute.params,
  );
  protected readonly loyalityProgramId = computed(
    () => this.routeParams()!['id'] as string,
  );
  protected readonly model = cachedComputed(
    () =>
      this._services.loyalityPrograms
        .data()
        .find((e) => e.id === this.loyalityProgramId()) ?? new LoyalityProgramVM(),
    (entry) => new LoyalityProgramVM(entry)
  );

  protected async submit(): Promise<void> {
    const session = this.model.session();
    const response = session.id
      ? await this._services.loyalityPrograms.update(session.id, session)
      : await this._services.loyalityPrograms.create('', session);

    if (response.success) {
      this.returnToList();
    }
  }

  protected returnToList() {
    this._services.router.navigate(['/modules/trotamundos/loyality-program']);
  }
}
