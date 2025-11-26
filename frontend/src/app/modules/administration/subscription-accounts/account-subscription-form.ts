import { Component, computed } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { ContextModel } from '@app/core/helpers/signal.helper';
import { Required } from '@core/directives/required';
import { Valid } from '@core/directives/valid';
import { TranslatePipe } from '@core/pipes/translate.pipe';
import { Button } from '@shared/ui/button/button';
import { ButtonDelete } from '@shared/ui/button/button-delete';
import { ButtonReturn } from '@shared/ui/button/button-return';
import { ButtonSubmit } from '@shared/ui/button/button-submit';
import { CheckboxInputComponent } from '@shared/ui/inputs/checkbox-input.component';
import { DropdownInputComponent } from '@shared/ui/inputs/dropdown-input.component';
import { TextInputComponent } from '@shared/ui/inputs/text-input.component';
import { UiPanel } from '@shared/ui/ui-panel';
import { UiTable } from '@shared/ui/ui-table';
import { UiTableColumn } from '@shared/ui/ui-table-column';
import { PermissionGroupVM } from '../permission-groups/permission-group.vm';
import { AccountSubscriptionDTO } from './account-subscription.dto';
import { injectSecuritySubscriptionAccounts } from './account-subscription.provider';
import { AccountSubscriptionVM } from './account-subscription.vm';

@Component({
    selector: 'app-account-subscription-form',
    hostDirectives: [{ directive: Valid, outputs: ['isValidChange'] }],
    imports: [
        UiPanel,
        Button,
        RouterLink,
        TranslatePipe,
        TextInputComponent,
        CheckboxInputComponent,
        DropdownInputComponent,
        Valid,
        Required,
        ButtonSubmit,
        ButtonReturn,
        ButtonDelete,
        UiTable,
        UiTableColumn
    ],
    template: `
    @if (model().session()) {
      <ng-container validate #validate="validate">
        <h1>{{ 'Subscription Accounts' | translate }}</h1>
        <app-ui-panel>
          <ng-template #start>
            <app-button
              submit
              [disabled]="!validate.isValid()"
              (buttonClick)="submit()"
              [isInProgress]="_services.subscriptions.saving()"
            />
          </ng-template>
          <ng-template #end>
            <app-button routerLink="../list" return />
          </ng-template>
        </app-ui-panel>

  <app-text-input
    [(value)]="model().session().email"
    [required]="true"
    label="Email"
    [disabled]="true"
  />
  <app-text-input
    [(value)]="model().session().subscription"
    [required]="true"
    label="Subscription"
    [disabled]="true"
  />

        <app-checkbox-input [(value)]="model().session().isActive" label="Active" />
        <app-checkbox-input [(value)]="model().session().isDefault" label="Default" />

        <h2>Permission Groups</h2>
        @if (availableGroups().length > 0) {
          <app-ui-panel>
            <ng-template #start>
              <app-dropdown-input
                #selectedGroup
                [data]="availableGroups()"
                textField="name"
                valueField="id"
              />
              <app-button
                [disabled]="!selectedGroup.value()"
                color="primary"
                text="Add Group"
                (buttonClick)="addGroup(selectedGroup)"
              />
            </ng-template>
          </app-ui-panel>
        }

        <app-ui-table [data]="assignedGroups()">
          <app-ui-table-column text="Group" field="name" />
          <app-ui-table-column text="Remove" field="id" width="150px">
            <ng-template #cell let-record="record">
              <app-button color="danger" text="Delete" (buttonClick)="removeGroup(record)" />
            </ng-template>
          </app-ui-table-column>
        </app-ui-table>

        @if (model().session().id) {
          <app-ui-panel>
            <ng-template #end>
              <app-button
                delete
                [facade]="{ identity: model().session().id, facade: _services.subscriptions }"
                (deleted)="returnToList()"
              />
            </ng-template>
          </app-ui-panel>
        }
      </ng-container>
    }
  `,
    styles: `:host { display: flex; flex-direction: column; gap: 1rem; }`
})
export class AccountSubscriptionForm {
    protected readonly _services = injectSecuritySubscriptionAccounts();
    protected readonly routeParams = toSignal(this._services.activatedRoute.params);
    protected readonly subscriptionId = computed(() => this.routeParams()?.['id'] as string);

    protected readonly model = computed(() => {
      return new ContextModel<AccountSubscriptionDTO>(
              () =>
          AccountSubscriptionDTO.fromVM(
            this._services.subscriptions
              .data()
              .find((e) => e.id === this.subscriptionId()) ??
              new AccountSubscriptionVM(),
          ),
              (entry) => new AccountSubscriptionDTO(entry)
          );
    });

    protected readonly assignedGroups = computed(() =>
      this._services.groups
        .data()
        .filter((g) => this.model().session().permissionGroups.includes(g.id)),
    );

    protected readonly availableGroups = computed(() =>
      this._services.groups
        .data()
        .filter((g) =>
          !this.model().session().permissionGroups.includes(g.id),
        ),
    );

    protected addGroup(selected: DropdownInputComponent<string>): void {
        if (!selected.value()) return;
        this.model().session().permissionGroups = [
          ...this.model().session().permissionGroups,
          selected.value()!,
        ];
        selected.value.set(null!);
        this.model().update();
    }

    protected removeGroup(group: PermissionGroupVM): void {
        this.model().session().permissionGroups = this.model()
          .session()
          .permissionGroups
          .filter((id) => id !== group.id);
        this.model().update();
    }

    protected async submit(): Promise<void> {
        const m = this.model().session();
        const response = m.id
          ? await this._services.subscriptions.update(m.id, m)
          : await this._services.subscriptions.create('', m);
        if (response.success) this.returnToList();
    }

  protected returnToList() {
    this._services.router.navigate([
      '/modules/administration/subscription-accounts',
    ]);
  }
}
