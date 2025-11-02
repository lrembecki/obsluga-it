import { NgTemplateOutlet } from '@angular/common';
import { Component, contentChildren, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-ui-panel',
  imports: [NgTemplateOutlet],
  template: `
    @if (start().length) {
      <div class="container">
        @for (item of start(); track item) {
          <ng-container [ngTemplateOutlet]="item" />
        }
      </div>
      <div class="fill"></div>
    }

    <ng-content />

    @if (end().length) {
      <div class="fill"></div>
      <div class="container">
        @for (item of end(); track item) {
          <ng-container [ngTemplateOutlet]="item" />
        }
      </div>
    }
  `,
  styles: `
    :host {
      display: flex;
      justify-content: space-between;
      margin-bottom: 1rem;
    }

    .fill {
      flex: auto;
    }

    .container {
      display: flex;
      gap: 1rem;
    }
  `,
})
export class UiPanel {
  start = contentChildren<TemplateRef<any>>('start');
  end = contentChildren<TemplateRef<any>>('end');
}
