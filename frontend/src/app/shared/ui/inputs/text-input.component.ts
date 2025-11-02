import { CommonModule } from '@angular/common';
import { Component, inject, input, model, output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Valid } from 'app/core/directives/valid';
import { InputTextModule } from 'primeng/inputtext';
import { HostControlDirective } from './host-control.directive';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, InputTextModule, CommonModule],
  selector: 'app-text-input',
  template: `
    @if (label()) {
      <label [for]="_id">{{ label() }}</label>
    }
    <input
      [type]="type()"
      [(ngModel)]="value"
      [disabled]="disabled()"
      (change)="valueChange.emit(value())"
      [id]="_id"
      pInputText
      [formControl]="hcd.control"
      [readOnly]="disabled()"
    />
  `,
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
export class TextInputComponent {
  protected readonly _id = `tb-${crypto.randomUUID()}`;
  protected readonly hcd = inject(HostControlDirective);
  public readonly label = input<string>();
  public readonly disabled = input<boolean>(false);
  public readonly value = model<string | undefined>();
  public readonly valueChange = output<string | undefined>();
  public readonly type = input<'text' | 'password' | 'email'>('text');

  ngOnInit(): void {}

  protected change($event: string) {
    this.value.set($event);
    this.valueChange.emit($event);
  }
}
