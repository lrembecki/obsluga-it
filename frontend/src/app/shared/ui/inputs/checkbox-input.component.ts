import { Component, inject, input, model, output } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckboxModule } from 'primeng/checkbox';
import { HostControlDirective } from './host-control.directive';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, CheckboxModule],
  selector: 'app-checkbox-input',
  template: `
    @if (label()) {
      <label [for]="_id">{{ label() }}</label>
    }
    <p-checkbox
      [inputId]="_id"
      [formControl]="hcd.control"
      [(ngModel)]="value"
      [binary]="true"
      (onChange)="valueChanged()"
    />
  `,
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
export class CheckboxInputComponent {
  protected readonly _id = `tb-${crypto.randomUUID()}`;
  hcd = inject(HostControlDirective);
  label = input<string>();
  public readonly value = model<boolean>(false);
  public readonly valueChange = output<boolean>();

  protected valueChanged() {
    this.valueChange.emit(this.value());
  }
}
