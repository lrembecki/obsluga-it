import { Component, computed, effect, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import { Button } from '../ui/button/button';
import { ButtonDelete } from '../ui/button/button-delete';
import { ButtonSubmit } from '../ui/button/button-submit';
import { UiPanel } from '../ui/ui-panel';
import { BaseFormComponent } from './base-form.component';
import { FormRenderer } from './form-renderer';
import { FormService } from './form.service';

@Component({
  selector: 'app-form-template',
  imports: [FormRenderer, UiPanel, Button, ButtonSubmit, ButtonDelete],
  template: `
    @if (form()) {
      <app-ui-panel>
        <ng-template #start>
          <app-button
            submit
            [disabled]="form()!.invalid"
            (buttonClick)="onSubmit()"
            [isInProgress]="_service.facade.saving()"
          />

          @if (mode === 'edit' && schema().canDelete(form()!.value)) {
            <app-button
              delete
              [facade]="{ identity: this.id(), facade: this._service.facade }"
              (deleted)="returnToList()"
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
  styles: ``,
})
export class FormTemplate extends BaseFormComponent<any> {
  readonly _service = inject(FormService<any>);
  readonly schema = computed(() => this._service.schema());
  readonly #returnRoute = computed(() => this._service.returnRoute());

  readonly #activatedRoute = inject(ActivatedRoute);
  readonly #router = inject(Router);
  readonly routeParams = toSignal(this.#activatedRoute.params);
  readonly id = computed(() => this.routeParams()!['id'] as string);
  readonly model = computed(
    () => (this._service.facade as any).getById(this.id())!,
  );

  constructor() {
    super();

    effect(() => {
      if (this.id() === 'create') {
        this.form.set(this.formFactory.createForm(this.schema()));
      } else {
        this.mode = 'edit';
        this.form.set(this.formFactory.createForm(this.schema(), this.mode));
        const model = this.model();
        if (model) {
          this.patch(model);
        }
      }
    });
  }

  ngOnInit(): void {
    this._service.facade.initialize().then(() => {
      this.form.set(this.formFactory.createForm(this.schema()));
    });
  }

  async submit(data: any): Promise<void> {
    const model = structuredClone(this.model()) ?? data;
    Object.assign(model, data);

    const response = this.id()
      ? await this._service.facade.update(this.id(), model)
      : await this._service.facade.create('', model);

    if (response.success) {
      this.returnToList();
    }
  }

  protected returnToList() {
    this.#router.navigate(this.#returnRoute());
  }
}
