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

    @if (end().length) {
      <div class="fill"></div>
      <div class="container">
        @for (item of end(); track item) {
          <ng-container [ngTemplateOutlet]="item" />
        }
      </div>
    }

    @if (!start().length && !end().length) {
      <div class="container">
        <ng-content />
      </div>
      <div class="fill"></div>
    }
  `,
  styles: `
    :host {
      display: flex;
      justify-content: space-between;
      background-color: var(--bg-head);
      border-bottom: 1px solid var(--border);
      padding: 0.5rem 1rem;
      margin-bottom: 1rem;
      transition:
        background-color 0.2s ease,
        border-color 0.2s ease;
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
