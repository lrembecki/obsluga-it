import { Component, computed, effect, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';
import { Button } from "../ui/button/button";
import { ButtonDelete } from "../ui/button/button-delete";
import { ButtonSubmit } from "../ui/button/button-submit";
import { UiPanel } from "../ui/ui-panel";
import { BaseFormComponent } from './base-form.component';
import { FormRenderer } from "./form-renderer";
import { FormFacadeScope, FormReturnRouteScope, FormSchemaScope } from './form-schema.model';

@Component({
  selector: 'app-form-template',
  imports: [FormRenderer, UiPanel, Button, ButtonSubmit, ButtonDelete],
  template: `
    <app-ui-panel>
      <ng-template #start>
        <app-button
          submit
          [disabled]="form.invalid"
          (buttonClick)="onSubmit()"
          [isInProgress]="facade.saving()"
        />

        @if (mode === 'edit' && schema.canDelete(form.value)) {
          <app-button
            delete
            [facade]="{ identity: this.id(), facade: facade }"
            (deleted)="returnToList()"
          />
        }

      </ng-template>
      <ng-template #end>
        <app-button (buttonClick)="returnToList()" return />
      </ng-template>
    </app-ui-panel>
    <app-form-renderer [schema]="schema" [form]="form" />
  `,
  styles: ``
})
export class FormTemplate extends BaseFormComponent<any> {
  readonly schema = inject(FormSchemaScope);
  readonly facade = inject(FormFacadeScope);
  readonly #returnRoute = inject(FormReturnRouteScope);

  readonly #activatedRoute = inject(ActivatedRoute);
  readonly #router = inject(Router);
  readonly routeParams = toSignal(this.#activatedRoute.params);
  readonly id = computed(() => this.routeParams()!['id'] as string);
  readonly model = computed(() => (this.facade as any).getById(this.id())!);

  constructor() {
    super();

    effect(() => {
      if (this.id()) {
        const model = this.model();
        this.mode = 'edit';
        this.form = this.formFactory.createForm(this.schema, this.mode);
        this.patch(model);
      }
    });
  }

  async submit(data: any): Promise<void> {

    const model = structuredClone(this.model()) ?? data;
    Object.assign(model, data);

    const response = this.id()
      ? await this.facade.update(this.id(), model)
      : await this.facade.create('', model);

    if (response.success) {
      this.returnToList();
    }
  }

  protected returnToList() {
    this.#router.navigate(this.#returnRoute);
  }
}
