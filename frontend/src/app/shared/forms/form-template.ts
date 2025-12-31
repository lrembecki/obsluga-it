import { Component, computed, effect, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import { Button } from '../ui/button/button';
import { ButtonDelete } from '../ui/button/button-delete';
import { ButtonReturn } from '../ui/button/button-return';
import { ButtonSubmit } from '../ui/button/button-submit';
import { LoadingComponent } from '../ui/loading/loading.component';
import { UiPanel } from '../ui/ui-panel';
import { BaseFormComponent } from './base-form.component';
import { FormRenderer } from './form-renderer';

@Component({
  selector: 'app-form-template',
  imports: [
    FormRenderer,
    UiPanel,
    Button,
    ButtonSubmit,
    ButtonDelete,
    ButtonReturn,
    LoadingComponent,
  ],
  template: `
    @if (_service.isLoading()) {
      <app-loading [text]="'Loading form data'" />
    } @else if (_service.form()) {
      <app-ui-panel>
        <ng-template #start>
          <app-button
            submit
            [disabled]="_service.form()!.invalid"
            [isInProgress]="_service.facade.saving()"
            (buttonClick)="onSubmit()"
          />

          @if (
            this._service.mode() === 'edit' &&
            this._service.schema().canDelete(this._service.form()!.value)
          ) {
            <app-button
              delete
              [isInProgress]="_service.facade.deleting()"
              (deleted)="remove()"
            />
          }
        </ng-template>
        <ng-template #end>
          <app-button (buttonClick)="returnToList()" return />
        </ng-template>
      </app-ui-panel>

      <app-form-renderer [schema]="schema()" [form]="_service.form()!" />
    }
  `,
  styles: `
    :host {
      display: block;
      position: relative;
      min-height: 200px;

      &:last-child {
        margin-bottom: 5rem;
      }
    }
  `,
})
export class FormTemplate extends BaseFormComponent<any> {
  readonly schema = computed(() => this._service.schema());
  readonly #returnRoute = computed(() => this._service.returnRoute());

  readonly #activatedRoute = inject(ActivatedRoute);
  readonly #router = inject(Router);
  readonly routeParams = toSignal(this.#activatedRoute.params);

  constructor() {
    super();
    effect(() => this._service.id.set(this.routeParams()!['id'] as string));
  }

  async submit(data: any): Promise<void> {
    const response = await this._service.submit(data);
    if (response) {
      this.returnToList();
    }
  }

  protected async remove(): Promise<void> {
    await this._service.facade.delete(this._service.id());
    this.returnToList();
  }

  protected returnToList() {
    const value = this.#returnRoute();
    console.log('Navigating to:', value);
    this.#router.navigate(this.#returnRoute());
  }
}
