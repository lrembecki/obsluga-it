import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Valid } from 'app/core/directives/valid';
import { cachedComputed } from 'app/core/helpers/signal.helper';
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
    @if (model.session()) {
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
          <app-checkbox-input [(value)]="model.session().isActive" label="Active" />
          <app-checkbox-input [(value)]="model.session().isDefault" label="Default" />

          <app-text-input [(value)]="model.session().smtpServer" label="SMTP Server" />
          <app-text-input [(value)]="model.session().smtpPort" label="SMTP Port" />
          <app-text-input
            [(value)]="model.session().smtpUsername"
            label="SMTP Username"
          />
          <app-text-input
            [(value)]="model.session().smtpPassword"
            label="SMTP Password"
            type="password"
          />
          <app-checkbox-input [(value)]="model.session().useSsl" label="Use SSL" />

          <!-- From -->
          <app-text-input
            [(value)]="model.session().fromAddress"
            label="From Address"
          />
          <app-text-input [(value)]="model.session().fromName" label="From Name" />

          <!-- Reply -->
          <app-text-input
            [(value)]="model.session().replyToAddress"
            label="Reply To Address"
          />
          <app-text-input
            [(value)]="model.session().replyToName"
            label="Reply To Name"
          />
        </form>
      </ng-container>
    }
  `,
})
export class EmailForm {
  private readonly _services = injectServices();
  protected readonly model = cachedComputed(
    () => this._services.emailSettings.data(),
    (entry) => ({ ...entry })
  );
  protected readonly saving = this._services.emailSettings.saving;

  async submit(): Promise<void> {
  const session = this.model.session();
  this._services.emailSettings.update('', session);
  }
}
