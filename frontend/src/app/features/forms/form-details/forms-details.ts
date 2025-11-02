import { Component, model } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { TabsModule } from 'primeng/tabs';

@Component({
  selector: 'app-forms-details',
  imports: [RouterOutlet, TabsModule, RouterLink, RouterLinkActive],
  template: `
    <p-tabs [value]="selectedValue()">
      <p-tablist>
        <p-tab
          value="0"
          [routerLink]="['.', 'requests']"
          routerLinkActive
          (isActiveChange)="isActive('0', $event)"
        >
          Requests
        </p-tab>
        <p-tab
          value="1"
          [routerLink]="['.', 'controls']"
          routerLinkActive
          (isActiveChange)="isActive('1', $event)"
        >
          Controls
        </p-tab>
      </p-tablist>
      <p-tabpanels>
        <p-tabpanel value="0"> <router-outlet /> </p-tabpanel>
        <p-tabpanel value="1"> <router-outlet /> </p-tabpanel>
      </p-tabpanels>
    </p-tabs>
  `,
  styles: ``,
})
export class FormsDetails {
  selectedValue = model('0');
  isActive(value: string, isActive: boolean) {
    if (isActive) {
      this.selectedValue.set(value);
    }
  }
}
