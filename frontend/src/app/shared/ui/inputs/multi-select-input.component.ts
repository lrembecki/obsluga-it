import { Component, inject, input, model, output } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MultiSelectFilterEvent, MultiSelectModule } from 'primeng/multiselect';
import { HostControlDirective } from './host-control.directive';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, MultiSelectModule],
  selector: 'app-multi-select-input',
  template: `
    <p-multiSelect
      [options]="data()"
      [formControl]="hcd.control"
      [placeholder]="placeholder()"
      [optionLabel]="textField()"
      (onFilter)="onFilter($event)"
      (keydown.enter)="onEnter()"
      [filterValue]="filterValue()"
      [(ngModel)]="value"
      (ngModelChange)="valueChange.emit(value())"
    />
  `,
  hostDirectives: [HostControlDirective],
  host: {
    class: 'input-container',
  },
})
export class MultiSelectInputComponent {
  public readonly hcd = inject(HostControlDirective);
  public readonly data = model<any[]>([]);
  public readonly textField = input<string | undefined>(undefined);
  public readonly placeholder = input<string>(null!);
  public readonly filterValue = model<string>('');
  public readonly loading = input(false);
  public readonly value = model<any[]>([]);
  public readonly valueChange = output<any[]>();
  public readonly addElement = output<string>();

  protected onFilter(event: MultiSelectFilterEvent): void {
    this.filterValue.set(event.filter);
  }

  protected onEnter(): void {
    if (this.filterValue()?.length) {
      this.addElement.emit(this.filterValue());
      this.filterValue.set('');
    }
  }
}
