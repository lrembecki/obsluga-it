import { Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { Required } from 'app/core/directives/required';
import { Valid } from 'app/core/directives/valid';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { ButtonDelete } from 'app/shared/ui/button/button-delete';
import { ButtonReturn } from 'app/shared/ui/button/button-return';
import { ButtonSubmit } from 'app/shared/ui/button/button-submit';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectTrotamundosHighlights } from './highlight.provider';
import { HighlightVM } from './highlight.vm';

@Component({
  selector: 'app-highlight-form',
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
    Valid,
    Required,
    ButtonSubmit,
    ButtonReturn,
    ButtonDelete,
  ],
  template: `
    @if (model()) {
      <ng-container validate #validate="validate">
        <h1>{{ 'Highlights' | translate }}</h1>
        <app-ui-panel>
          <ng-template #start>
            <app-button
              submit
              [disabled]="!validate.isValid()"
              (buttonClick)="submit()"
              [isInProgress]="_services.highlights.saving()"
            />
          </ng-template>
          <ng-template #end>
            <app-button routerLink="../list" return />
          </ng-template>
        </app-ui-panel>
        <app-text-input
          [(value)]="model().title"
          [required]="true"
          label="Title"
        />
        <app-text-input
          [(value)]="model().icon"
          [required]="true"
          label="Icon"
        />
        @if (model().id) {
          <app-ui-panel>
            <ng-template #end>
              <app-button
                delete
                [facade]="{
                  identity: model().id,
                  facade: _services.highlights,
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
export class HighlightForm {
  protected readonly _services = injectTrotamundosHighlights();
  protected readonly routeParams = toSignal(
    this._services.activatedRoute.params,
  );
  protected readonly highlightId = computed(
    () => this.routeParams()!['id'] as string,
  );
  protected readonly model = computed(
    () =>
      this._services.highlights
        .data()
        .find((e) => e.id === this.highlightId()) ?? new HighlightVM(),
  );

  protected async submit(): Promise<void> {
    const model = this.model();

    const response = model.id
      ? await this._services.highlights.update(model.id, model)
      : await this._services.highlights.create('', model);

    if (response.success) {
      this.returnToList();
    }
  }

  protected returnToList() {
    this._services.router.navigate(['/modules/trotamundos/highlights']);
  }
}
