import { CommonModule } from '@angular/common';
import { Component, inject, input, model, output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Valid } from 'app/core/directives/valid';
import { TextareaModule } from 'primeng/textarea';
import { HostControlDirective } from './host-control.directive';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, TextareaModule, CommonModule, FormsModule],
  selector: 'app-textarea-input',
  template: ` @if (label()) {
      <label [for]="_id">{{ label() }}</label>
    }
    <textarea
      [rows]="rows()"
      [cols]="cols()"
      class="textarea-input"
      pTextarea
      [(ngModel)]="value"
      (change)="valueChange.emit(value())"
      [formControl]="hcd.control"
      [readonly]="disabled()"
    style="margin-bottom: .5rem;"
    ></textarea>`,
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
    :host, .textarea-input {
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

  protected change($event: string) {
    this.value.set($event);
    this.valueChange.emit($event);
  }
}
