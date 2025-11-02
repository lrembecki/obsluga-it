import { NgTemplateOutlet } from '@angular/common';
import {
  Component,
  contentChild,
  input,
  model,
  TemplateRef,
} from '@angular/core';
import { Dialog } from 'primeng/dialog';

@Component({
  selector: 'app-ui-dialog',
  imports: [Dialog, NgTemplateOutlet],
  template: `
    <p-dialog
      [(visible)]="visible"
      [modal]="true"
      [style]="{ 'min-width': '25rem' }"
      [position]="position()"
    >
      <ng-template #header>
        <div class="inline-flex items-center justify-center gap-2">
          <span class="font-bold whitespace-nowrap">{{ title() }}</span>
        </div>
      </ng-template>
      <ng-content />

      @if (footerTemplate()) {
        <ng-template #footer>
          <ng-container
            [ngTemplateOutlet]="footerTemplate()"
            [ngTemplateOutletContext]="{ dialog: this }"
          />
        </ng-template>
      }
    </p-dialog>
  `,
  styles: ``,
})
export class UiDialog {
  visible = model(false);
  title = input.required<string>();
  position = input<'top'>('top');

  footerTemplate = contentChild<TemplateRef<any>>('footer');

  show() {
    this.visible.set(true);
  }
  close() {
    this.visible.set(false);
  }
}
