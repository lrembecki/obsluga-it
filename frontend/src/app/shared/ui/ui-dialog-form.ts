import { Component, input, model, output } from '@angular/core';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { Button } from './button/button';
import { UiDialog } from './ui-dialog';
import { UiPanel } from './ui-panel';

@Component({
  selector: 'app-ui-dialog-form',
  imports: [UiDialog, UiPanel, Button, TranslatePipe, ProgressSpinnerModule],
  template: `
    @if (visible()) {
      <app-ui-dialog [(visible)]="visible" [title]="title()">
        <ng-content />
        <ng-template #footer>
          <app-ui-panel>
            <ng-template #end>
              <app-button
                color="primary"
                [disabled]="!valid()"
                [text]="'Submit' | translate"
                [isInProgress]="saving()"
                (buttonClick)="submit.emit(this)"
              />
            </ng-template>
          </app-ui-panel>
        </ng-template>
      </app-ui-dialog>
    }
  `,
  styles: ``,
})
export class UiDialogForm extends UiDialog {
  submit = output<this>();
  saving = model(false);
  valid = input(true);
}
