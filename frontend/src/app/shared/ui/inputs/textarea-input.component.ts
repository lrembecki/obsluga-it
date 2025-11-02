import { Component, inject, input, model, output } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { TextareaModule } from 'primeng/textarea';
import { HostControlDirective } from './host-control.directive';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, TextareaModule],
  selector: 'app-textarea-input',
  template: ` @if (label()) {
      <label [for]="_id">{{ label() }}</label>
    }
    <textarea
      [rows]="rows()"
      [cols]="cols()"
      pTextarea
      [formControl]="hcd.control"
      [readonly]="disabled()"
    ></textarea>`,
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
export class TextareaInputComponent {
  rows = input<number>(5);
  cols = input<number>(30);
  protected readonly _id = `tb-${crypto.randomUUID()}`;
  protected readonly hcd = inject(HostControlDirective);
  public readonly label = input<string>();
  public readonly disabled = input<boolean>(false);
  public readonly value = model<string | undefined>();
  public readonly valueChange = output<string | undefined>();
}
