import { Component, inject, input, model } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { FloatLabel } from 'primeng/floatlabel';
import { InputNumberModule } from 'primeng/inputnumber';
import { HostControlDirective } from './host-control.directive';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, InputNumberModule, FloatLabel],
  selector: 'app-number-input',
  template: `
    <p-floatlabel>
      <p-inputNumber
        [(ngModel)]="value"
        [formControl]="hcd.control"
        [disabled]="disabled()"
      ></p-inputNumber>
      <label [for]="_id">{{ label() }}</label>
    </p-floatlabel>
  `,
  hostDirectives: [HostControlDirective],
  host: {
    class: 'input-container',
  },
})
export class NumberInputComponent {
  protected readonly _id = `tb-${crypto.randomUUID()}`;
  protected readonly hcd = inject(HostControlDirective);
  public readonly label = input<string>();
  public readonly disabled = input<boolean>(false);
  public readonly value = model<number | undefined | null>();

  ngOnInit() {
    if (
      this.hcd?.control?.value !== null &&
      this.hcd?.control?.value !== undefined
    ) {
      this.value.set(this.hcd.control.value);
    }
  }
}
