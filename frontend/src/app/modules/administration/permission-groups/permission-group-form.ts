import { Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { Required } from 'app/core/directives/required';
import { Valid } from 'app/core/directives/valid';
import { cachedComputed } from 'app/core/helpers/signal.helper';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button, Button as UiButton } from 'app/shared/ui/button/button';
import { ButtonDelete } from 'app/shared/ui/button/button-delete';
import { ButtonReturn } from 'app/shared/ui/button/button-return';
import { ButtonSubmit } from 'app/shared/ui/button/button-submit';
import { DropdownInputComponent } from "app/shared/ui/inputs/dropdown-input.component";
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable } from 'app/shared/ui/ui-table';
import { UiTableColumn } from 'app/shared/ui/ui-table-column';
import { PermissionVM } from '../permissions/permission.vm';
import { PermissionGroupDTO } from './permission-group.dto';
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
    UiButton,
    DropdownInputComponent
  ],
  template: `
    @if (model.session()) {
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
          </ng-template>
          <ng-template #end>
            <app-button routerLink="../list" return />
          </ng-template>
        </app-ui-panel>

        <app-text-input
          [(value)]="model.session().name"
          [required]="true"
          label="Name"
        />

        <h2>Permissions</h2>

        @if (availablePermissions().length > 0) {
          <app-ui-panel>
            <ng-template #start>
              <app-dropdown-input #selectedPermission [data]="availablePermissions()" textField="name" valueField="id" />
              <app-button [disabled]="!selectedPermission.value()" color="primary" text="Add Permission" (buttonClick)="addPermission(selectedPermission)" />
            </ng-template>
          </app-ui-panel>
        }

        <app-ui-table [data]="assignedPermissions()">
          <app-ui-table-column
            text="Permission"
            field="name"
          />
          <app-ui-table-column text="Remove" field="id" width="150px">
            <ng-template #cell let-record="record">
              <app-button color="danger" text="Delete" (buttonClick)="removePermission(record)" />
            </ng-template>
          </app-ui-table-column>
        </app-ui-table>

        @if (model.session().id) {
          <app-ui-panel>
            <ng-template #end>
              <app-button
                delete
                [facade]="{
                  identity: model.session().id,
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
  styles: `
    :host {
      display: flex;
      flex-direction: column;
      gap: 1rem;
    }
  `
})
export class PermissionGroupForm {
  protected readonly _services = injectSecurityPermissionGroups();
  protected readonly routeParams = toSignal(
    this._services.activatedRoute.params,
  );
  protected readonly groupId = computed(
    () => this.routeParams()!['id'] as string,
  );

  protected readonly model = cachedComputed(() =>
      PermissionGroupDTO.fromVM(this._services.groups
        .data()
        .find((e) => e.id === this.groupId()) ?? new PermissionGroupVM()),
      entry => new PermissionGroupDTO(entry)
    );
  
  protected readonly assignedPermissions = computed(() => this._services.permissions.data().filter(p => this.model.session().permissions.includes(p.id)));
  protected readonly availablePermissions = computed(() => this._services.permissions.data().filter(p => !this.model.session().permissions.includes(p.id)));

  protected addPermission(selectedPermission: DropdownInputComponent<string>): void {
    if (selectedPermission.value()) {
      this.model.session().permissions.push(selectedPermission.value()!);
      selectedPermission.value.set(null!);
      this.model.update();
    }
  }

  protected removePermission(permission: PermissionVM): void {

    console.log({ removing: permission, from: this.model.session().permissions });
    const permissions = this.model.session().permissions;

    this.model.session().permissions = [
      ...permissions.filter((p) => p !== permission.id),
    ];

    this.model.update();
  }

  protected async submit(): Promise<void> {
    const model = this.model.session();

    const response = model.id
      ? await this._services.groups.update(model.id, model)
      : await this._services.groups.create('', model);

    if (response.success) {
      this.returnToList();
    }
  }

  protected returnToList() {
    this._services.router.navigate(['/modules/administration/permission-groups']);
  }
}
