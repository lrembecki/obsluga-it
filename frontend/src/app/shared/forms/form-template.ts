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
    @if (_service.form()) {
      <app-ui-panel>
        <ng-template #start>
          <app-button
            submit
            [disabled]="_service.form()!.invalid"
            (buttonClick)="onSubmit()"
            [isInProgress]="_service.facade.saving()"
          />

          @if (
            this._service.mode() === 'edit' &&
            this._service.schema().canDelete(this._service.form()!.value)
          ) {
            <app-button
              delete
              [facade]="{
                identity: this._service.id(),
                facade: this._service.facade,
              }"
              (deleted)="returnToList()"
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
  styles: ``,
})
export class FormTemplate extends BaseFormComponent<any> {
  readonly _service = inject(FormService<any>);
  readonly schema = computed(() => this._service.schema());
  readonly #returnRoute = computed(() => this._service.returnRoute());

  readonly #activatedRoute = inject(ActivatedRoute);
  readonly #router = inject(Router);
  readonly routeParams = toSignal(this.#activatedRoute.params);

  constructor() {
    super();
    effect(() => this._service.id.set(this.routeParams()!['id'] as string));
    effect(() => this.form.set(this._service.form()));
  }

  ngOnInit(): void {
    this._service.facade.initialize().then(() => {
      this.form.set(this.formFactory.createForm(this.schema()));
    });
  }

  async submit(data: any): Promise<void> {
    const response = await this._service.submit(data);
    if (response) {
      this.returnToList();
    }
  }

  protected returnToList() {
    this.#router.navigate(this.#returnRoute());
  }
}
