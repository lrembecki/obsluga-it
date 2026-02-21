import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToastComponent } from '@obsluga-it/shared/ui-kit';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ToastComponent],
  template: `
    <ui-toast />
    <router-outlet />
  `,
})
export class AppComponent {}
