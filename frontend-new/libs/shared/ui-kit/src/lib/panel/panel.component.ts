import { Component } from '@angular/core';

@Component({
  selector: 'app-ui-panel',
  standalone: true,
  template: `
    <div class="panel">
      <div class="panel-start">
        <ng-content select="[start]" />
      </div>
      <div class="panel-end">
        <ng-content select="[end]" />
      </div>
    </div>
  `,
  styles: [`
    .panel {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 0.5rem 0;
      margin-bottom: 1rem;
    }
    .panel-start, .panel-end {
      display: flex;
      gap: 0.5rem;
      align-items: center;
    }
  `],
})
export class UiPanel {}
