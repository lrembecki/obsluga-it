import { Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { Required } from 'app/core/directives/required';
import { Valid } from 'app/core/directives/valid';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button, Button as UiButton } from 'app/shared/ui/button/button';
import { ButtonDelete } from 'app/shared/ui/button/button-delete';
import { ButtonReturn } from 'app/shared/ui/button/button-return';
import { ButtonSubmit } from 'app/shared/ui/button/button-submit';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { PermissionVM } from '../permissions/permission.vm';
import { injectSecurityPermissionGroups } from './permission-group.provider';
import { PermissionGroupVM } from './permission-group.vm';

@Component({
  selector: 'app-permission-group-form',
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
    UiTable,
    UiTableColumn,
    UiButton
  ],
  template: `
    @if (model()) {
      <ng-container validate #validate="validate">
        <h1>{{ 'Permission Groups' | translate }}</h1>
        <app-ui-panel>
          <ng-template #start>
            <app-button
              submit
              [disabled]="!validate.isValid()"
              (buttonClick)="submit()"
              [isInProgress]="_services.groups.saving()"
            />
            <app-button color="primary" text="Add Permission" (buttonClick)="addPermission()" />
          </ng-template>
          <ng-template #end>
            <app-button routerLink="../list" return />
          </ng-template>
        </app-ui-panel>
        <app-text-input
          [disabled]="!!model().id"
          [(value)]="model().name"
          [required]="true"
          label="Name"
        />

            <app-ui-table [data]="model().permissions">
              <app-ui-table-column
                text="Permission"
                field="name" width="300px"
              />
              <app-ui-table-column text="Remove" field="id" width="150px">
                <ng-template #cell let-record>
                  <app-button text="Remove" (buttonClick)="removePermission(record)" />
                </ng-template>
              </app-ui-table-column>
            </app-ui-table>

        @if (model().id) {
          <app-ui-panel>
            <ng-template #end>
              <app-button
                delete
                [facade]="{
                  identity: model().id,
                  facade: _services.groups,
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
export class PermissionGroupForm {
  protected readonly _services = injectSecurityPermissionGroups();
  protected readonly routeParams = toSignal(
    this._services.activatedRoute.params,
  );
  protected readonly groupId = computed(
    () => this.routeParams()!['id'] as string,
  );
  protected readonly model = computed(
    () =>
      this._services.groups
        .data()
        .find((e) => e.id === this.groupId()) ?? new PermissionGroupVM(),
  );

  protected availablePermissions(): PermissionVM[] {
    return this._services.permissions
      .data()
      .filter((p) => !this.model().permissions.some((mp) => mp.id === p.id));
  }

  protected addPermission(): void {
    const next = this.availablePermissions()[0];
    if (!next) return;
    this.model().permissions.push(next);
  }

  protected removePermission(permission: PermissionVM): void {
    const permissions = this.model().permissions;
    const index = permissions.findIndex((p) => p.id === permission.id);
    if (index >= 0) {
      permissions.splice(index, 1);
    }
  }

  protected async submit(): Promise<void> {
    const model = this.model();

    const response = model.id
      ? await this._services.groups.update(model.id, model)
      : await this._services.groups.create('', model);

    if (response.success) {
      this.returnToList();
    }
  }

  protected returnToList() {
    this._services.router.navigate(['/modules/security/permission-groups']);
  }
}
