import { Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Required } from 'app/core/directives/required';
import { Valid } from 'app/core/directives/valid';
import { cachedComputed } from 'app/core/helpers/signal.helper';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { ButtonDelete } from 'app/shared/ui/button/button-delete';
import { ButtonReturn } from 'app/shared/ui/button/button-return';
import { ButtonSubmit } from 'app/shared/ui/button/button-submit';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { TextareaInputComponent } from "app/shared/ui/inputs/textarea-input.component";
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectTrotamundosAdvantages } from './advantage.provider';
import { AdvantageVM } from './advantage.vm';

@Component({
  selector: 'app-advantage-form',
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
    Valid,
    Required,
    ButtonSubmit,
    ButtonReturn,
    ButtonDelete,
    TextareaInputComponent
  ],
  template: `
    @if (model.session()) {
      <ng-container validate #validate="validate">
        <h1>{{ 'Contacts' | translate }}</h1>
        <app-ui-panel>
          <ng-template #start>
            <app-button
              submit
              [disabled]="!validate.isValid()"
              (buttonClick)="submit()"
              [isInProgress]="_services.advantages.saving()"
            />
          </ng-template>
          <ng-template #end>
            <app-button routerLink="../list" return />
          </ng-template>
        </app-ui-panel>
        
        <app-text-input
          [disabled]="!!model.session().id"
          [(value)]="model.session().title"
          [required]="true"
          label="Title"
        />

  <app-textarea-input [(value)]="model.session().content" [required]="true" label="Content" />

  @if (model.session().id) {
          <app-ui-panel>
            <ng-template #end>
              <app-button
                delete
                [facade]="{
                  identity: model.session().id,
                  facade: _services.advantages,
                }"
                (deleted)="returnToList()"
              />
            </ng-template>
          </app-ui-panel>
        }
      </ng-container>
    }
  `,
  styles: ``
})
export class AdvantageForm {
  protected readonly _services = injectTrotamundosAdvantages();
  protected readonly routeParams = toSignal(
    this._services.activatedRoute.params,
  );
  protected readonly advantageId = computed(
    () => this.routeParams()!['id'] as string,
  );
  protected readonly model = cachedComputed(
    () =>
      this._services.advantages
        .data()
        .find((e) => e.id === this.advantageId()) ?? new AdvantageVM(),
    (entry) => new AdvantageVM(entry)
  );

  protected async submit(): Promise<void> {
    const session = this.model.session();
    const response = session.id
      ? await this._services.advantages.update(session.id, session)
      : await this._services.advantages.create('', session);

    if (response.success) {
      this.returnToList();
    }
  }

  protected returnToList() {
    this._services.router.navigate(['/modules/trotamundos/advantages']);
  }
}
