import { Component, contentChildren, model } from '@angular/core';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Button } from 'app/shared/ui/button/button';
import { UiPanel } from 'app/shared/ui/ui-panel';
import { ButtonDelete } from '../ui/button/button-delete';

@Component({
  selector: 'app-form-template',
  imports: [UiPanel, Button, ReactiveFormsModule, ButtonDelete],
  template: `
    @if (formGroup()) {
      <app-ui-panel>
        <app-button text="Submit" />
        <app-button delete />
      </app-ui-panel>
      <form [formGroup]="formGroup()!"></form>
    }
  `,
  styles: ``,
})
export class FormTemplate {
  public readonly formGroup = model<FormGroup>();
  protected readonly panelStartElements = contentChildren('start');
  protected readonly panelEndElements = contentChildren('end');
}
