import { Component, inject, input, model, output } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Valid } from 'app/core/directives/valid';
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
      [ngModel]="value()"
      [disabled]="disabled()"
      (ngModelChange)="change($event)"
    />`,
  hostDirectives: [
    HostControlDirective,
    {
      directive: Valid,
      inputs: ['valid'],
    },
  ],
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
  public readonly value = model<Date | undefined>();
  public readonly valueChange = output<Date | undefined>();

  ngOnInit(): void {}

   
  protected change(_$event: Date) {
    if (_$event) _$event = new Date(_$event);

    const currentValue = this.value()?.toISOString() ?? '';
    const newValue = _$event?.toISOString() ?? '';

    if (currentValue === newValue) return;

    this.value.set(_$event);
    this.valueChange.emit(_$event);
  }
}
