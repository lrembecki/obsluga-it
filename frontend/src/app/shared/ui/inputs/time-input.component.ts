import { Component, inject } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HostControlDirective } from './host-control.directive';
import { DatePickerModule } from 'primeng/datepicker';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, DatePickerModule],
  selector: 'app-time-input',
  template: `
    <p-datepicker
      inputId="calendar-timeonly"
      [formControl]="hcd.control"
      [timeOnly]="true"
    />
  `,
  providers: [HostControlDirective],
  host: {
    class: 'input-container',
  },
})
export class TimeInputComponent {
  hcd = inject(HostControlDirective);
}
