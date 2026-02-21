import { Component } from '@angular/core';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'ui-toast',
  standalone: true,
  imports: [ToastModule],
  template: `<p-toast />`,
})
export class ToastComponent {}
