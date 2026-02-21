import { Component, computed, effect, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseFormComponent } from './base-form.component';
import { FormTemplateModelProvider } from './form-model.provider';
import { FormRenderer } from './form-renderer';

@Component({
  selector: 'app-form-template',
  standalone: true,
  imports: [FormRenderer],
  template: `
    @if (_service.isLoading()) {
      <p>Loading form data...</p>
    } @else if (form()) {
      <div class="form-actions-top">
        <button
          type="button"
          [disabled]="form()!.invalid"
          (click)="onSubmit()"
          class="btn-submit"
        >
          Save
        </button>

        @if (_service.mode() === 'edit' && _service.schema().canDelete(form()!.value)) {
          <button type="button" (click)="remove()" class="btn-delete">
            Delete
          </button>
        }

        <button type="button" (click)="returnToList()" class="btn-return">
          Return
        </button>
      </div>

      <app-form-renderer [schema]="schema()" [form]="form()!" />
    }
  `,
  styles: [`
    :host {
      display: block;
      position: relative;
      min-height: 200px;
    }
    .form-actions-top {
      display: flex;
      gap: 0.5rem;
      margin-bottom: 1rem;
    }
    .btn-submit {
      padding: 0.5rem 1rem;
      background: var(--primary);
      color: white;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }
    .btn-delete {
      padding: 0.5rem 1rem;
      background: var(--danger, #dc3545);
      color: white;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }
    .btn-return {
      padding: 0.5rem 1rem;
      background: var(--secondary, #6c757d);
      color: white;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }
    .btn-submit:disabled {
      opacity: 0.5;
      cursor: not-allowed;
    }
  `],
})
export class FormTemplate extends BaseFormComponent<any> {
  private readonly _modelProvider = inject(FormTemplateModelProvider);
  readonly schema = computed(() => this._service.schema());
  readonly #returnRoute = computed(() => this._service.returnRoute());

  readonly #activatedRoute = inject(ActivatedRoute);
  readonly #router = inject(Router);
  readonly routeParams = toSignal(this.#activatedRoute.params);

  constructor() {
    super();
    effect(() => {
      const params = this.routeParams();
      if (params) {
        this._modelProvider.id.set(params['id'] as string);
        this._modelProvider.mode.set(
          params['id'] === 'create' ? 'create' : 'edit',
        );
      }
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
    await this._service.facade.delete(this._modelProvider.id()!);
    this.returnToList();
  }

  protected returnToList() {
    this.#router.navigate(this.#returnRoute());
  }
}
