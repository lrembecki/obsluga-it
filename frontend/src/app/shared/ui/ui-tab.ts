import { NgTemplateOutlet } from '@angular/common';
import {
  Component,
  contentChildren,
  Directive,
  input,
  model,
  TemplateRef,
} from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { TabsModule } from 'primeng/tabs';

@Directive({
  selector: 'ng-template[ui-tab-page]',
})
export class UiTabPage {
  public readonly header = input.required<string>();
  public readonly isActive = model(false);
}

@Component({
  selector: 'app-ui-tab',
  imports: [ButtonModule, TabsModule, NgTemplateOutlet],
  template: `
    <p-tabs [value]="value">
      <p-tablist>
        @for (item of pages(); track item; let index = $index) {
          <p-tab [value]="index">{{ item.header() }}</p-tab>
        }
      </p-tablist>
      <p-tabpanels>
        @for (item of items(); track item; let index = $index) {
          <p-tabpanel [value]="index">
            <ng-container [ngTemplateOutlet]="item" />
          </p-tabpanel>
        }
      </p-tabpanels>
    </p-tabs>
  `,
  styles: ``,
})
export class UiTab {
  value: number = 0;
  items = contentChildren<TemplateRef<any>>('item');
  pages = contentChildren<UiTabPage>(UiTabPage);
}
