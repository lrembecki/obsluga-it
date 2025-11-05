import { Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
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
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectSecurityPermissions } from './permission.provider';
import { PermissionVM } from './permission.vm';

@Component({
  selector: 'app-permission-form',
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
    @if (model.session()) {
      <ng-container validate #validate="validate">
        <h1>{{ 'Permissions' | translate }}</h1>
        <app-ui-panel>
          <ng-template #start>
            <app-button
              submit
              [disabled]="!validate.isValid()"
              (buttonClick)="submit()"
              [isInProgress]="_services.permissions.saving()"
            />
          </ng-template>
          <ng-template #end>
            <app-button routerLink="../list" return />
          </ng-template>
        </app-ui-panel>
        <app-text-input
          [disabled]="!!model.session().id"
          [(value)]="model.session().name"
          [required]="true"
          label="Name"
        />
        @if (model.session().id) {
          <app-ui-panel>
            <ng-template #end>
              <app-button
                delete
                [facade]="{
                  identity: model.session().id,
                  facade: _services.permissions,
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
export class PermissionForm {
  protected readonly _services = injectSecurityPermissions();
  protected readonly routeParams = toSignal(
    this._services.activatedRoute.params,
  );
  protected readonly permissionId = computed(
    () => this.routeParams()!['id'] as string,
  );
  protected readonly model = cachedComputed(
    () =>
      this._services.permissions
        .data()
        .find((e) => e.id === this.permissionId()) ?? new PermissionVM(),
    (entry) => new PermissionVM(entry)
  );

  protected async submit(): Promise<void> {
    const session = this.model.session();
    const response = session.id
      ? await this._services.permissions.update(session.id, session)
      : await this._services.permissions.create('', session);

    if (response.success) {
      this.returnToList();
    }
  }

  protected returnToList() {
    this._services.router.navigate(['/modules/administration/permissions']);
  }
}
