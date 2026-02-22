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
import { FormTemplateModelProvider } from './form-model.provider';
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
    } @else if (form()) {
      <app-ui-panel>
        <ng-template #start>
          <app-button
            submit
            [disabled]="form()!.invalid"
            [isInProgress]="_service.facade.saving()"
            (buttonClick)="onSubmit()"
          />

          @if (
            this._service
              .schema()
              .canDelete(this._modelProvider.mode(), this.form()!.value)
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

      <app-form-renderer [schema]="schema()" [form]="form()!" />
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
  readonly _modelProvider = inject(FormTemplateModelProvider);
  readonly schema = computed(() => this._service.schema());
  readonly #returnRoute = computed(() => this._service.returnRoute());

  readonly #activatedRoute = inject(ActivatedRoute);
  readonly #router = inject(Router);
  readonly routeParams = toSignal(this.#activatedRoute.params);

  constructor() {
    super();
    effect(() => {
      this._modelProvider.id.set(this.routeParams()!['id'] as string);
      this._modelProvider.mode.set(
        this.routeParams()!['id'] === 'create' ? 'create' : 'edit',
      );

      console.log({
        id: this._modelProvider.id(),
        mode: this._modelProvider.mode(),
      });
    });
  }

  async submit(data: any): Promise<void> {
    const response = await this.executeSubmit(data);
    if (response) {
      this.returnToList();
    }
  }

  protected async executeSubmit(data: any): Promise<boolean> {
    const model = structuredClone(this._service.model()) ?? data;
    Object.assign(model, data);

    const response =
      this._modelProvider.mode() === 'create'
        ? await this._service.facade.create('', model)
        : await this._service.facade.update(
            this._modelProvider.id() ?? '',
            model,
          );

    return response.success;
  }

  protected async remove(): Promise<void> {
    await this._service.facade.delete(this._modelProvider.id());
    this.returnToList();
  }

  protected returnToList() {
    const value = this.#returnRoute();
    console.log('Navigating to:', value);
    this.#router.navigate(this.#returnRoute());
  }
}
