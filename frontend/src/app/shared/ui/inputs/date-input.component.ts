import { Component, inject, input, model, output } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DatePickerModule } from 'primeng/datepicker';
import { HostControlDirective } from './host-control.directive';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, DatePickerModule],
  selector: 'app-date-input',
  template: ` @if (label()) {
      <label [for]="_id">{{ label() }}</label>
    }
    <p-datepicker
      [formControl]="hcd.control"
      [id]="_id"
      [(ngModel)]="value"
      [disabled]="disabled()"
    />`,
  hostDirectives: [HostControlDirective],
  host: {
    class: 'input-container',
  },
  styles: `
    :host {
      display: flex;
      flex-direction: column;
      gap: 0.25rem;
    }
  `,
})
export class DateInputComponent {
  protected readonly _id = `dt-${crypto.randomUUID()}`;
  protected readonly hcd = inject(HostControlDirective);
  public readonly label = input<string>();
  public readonly disabled = input<boolean>(false);
  public readonly value = model<string | undefined>();
  public readonly valueChange = output<string | undefined>();

  ngOnInit() {
    if (this.hcd?.control) {
      this.value.set(this.hcd.control.value);
    }
  }
}
