import { Component } from '@angular/core';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { Button } from 'app/shared/ui/button/button';
import { UiDialogForm } from 'app/shared/ui/ui-dialog-form';
import { GroupForm } from './group-form';

@Component({
  selector: 'app-group-form-button',
  imports: [Button, UiDialogForm, GroupForm, TranslatePipe],
  template: `
    <app-ui-dialog-form
      #addGroupDialog
      [title]="'Add Group' | translate"
      [valid]="groupForm.isValid()"
    >
      <app-group-form #groupForm [dialog]="addGroupDialog" />
    </app-ui-dialog-form>

    <app-button
      [text]="'Add Group' | translate"
      (buttonClick)="addGroupDialog.show()"
    />
  `,
  styles: ``,
})
export class GroupFormButton { }
