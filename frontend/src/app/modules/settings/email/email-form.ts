import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Valid } from 'app/core/directives/valid';
import { Button } from 'app/shared/ui/button/button';
import { CheckboxInputComponent } from 'app/shared/ui/inputs/checkbox-input.component';
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { injectServices } from './email-settings.provider';

@Component({
  selector: 'app-email-form',
  imports: [
    ReactiveFormsModule,
    TextInputComponent,
    CheckboxInputComponent,
    UiPanel,
    Button,
    Valid,
  ],
  template: `
    @if (model()) {
      <ng-container validate #validate="validate">
        <app-ui-panel>
          <ng-template #start>
            <app-button
              text="Submit"
              color="primary"
              [disabled]="!validate.isValid()"
              [isInProgress]="saving()"
              (buttonClick)="submit()"
            />
          </ng-template>
        </app-ui-panel>

        <form>
          <app-checkbox-input [(value)]="model().isActive" label="Active" />
          <app-checkbox-input [(value)]="model().isDefault" label="Default" />

          <app-text-input [(value)]="model().smtpServer" label="SMTP Server" />
          <app-text-input [(value)]="model().smtpPort" label="SMTP Port" />
          <app-text-input
            [(value)]="model().smtpUsername"
            label="SMTP Username"
          />
          <app-text-input
            [(value)]="model().smtpPassword"
            label="SMTP Password"
            type="password"
          />
          <app-checkbox-input [(value)]="model().useSsl" label="Use SSL" />

          <!-- From -->
          <app-text-input
            [(value)]="model().fromAddress"
            label="From Address"
          />
          <app-text-input [(value)]="model().fromName" label="From Name" />

          <!-- Reply -->
          <app-text-input
            [(value)]="model().replyToAddress"
            label="Reply To Address"
          />
          <app-text-input
            [(value)]="model().replyToName"
            label="Reply To Name"
          />
        </form>
      </ng-container>
    }
  `,
})
export class EmailForm {
  private readonly _services = injectServices();
  protected readonly model = this._services.emailSettings.data;
  protected readonly saving = this._services.emailSettings.saving;

  async submit(): Promise<void> {
    this._services.emailSettings.update('', this.model());
  }
}
