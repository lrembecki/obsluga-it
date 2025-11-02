import { NgTemplateOutlet } from '@angular/common';
import {
  Component,
  contentChild,
  inject,
  input,
  model,
  output,
  TemplateRef,
} from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { fieldValue } from 'app/core/helpers/field.helper';
import { SelectChangeEvent, SelectModule } from 'primeng/select';
import { HostControlDirective } from './host-control.directive';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, SelectModule, NgTemplateOutlet],
  selector: 'app-dropdown-input',
  template: `
    @if (label()) {
      <label [for]="_id">{{ label() }}</label>
    }
    <p-select
      [id]="_id"
      [options]="data()"
      [(ngModel)]="value"
      [formControl]="hcd.control"
      [optionLabel]="textField()"
      [placeholder]="placeholder()"
      [ngModel]="value()"
      [optionValue]="valueField()"
      (onChange)="onChange($event)"
      [showClear]="clearable()"
      class="w-full md:w-56"
    >
      @if (itemContentTemplate()) {
        <ng-template #selectedItem let-selectedOption>
          @if (selectedOption) {
            <div class="flex items-center gap-2">
              <ng-container
                [ngTemplateOutlet]="itemContentTemplate()"
                [ngTemplateOutletContext]="{ item: selectedOption }"
              />
            </div>
          } @else {
            {{ renderValue(selectedItem, textField()!) }}
          }
        </ng-template>
        <ng-template let-item #item>
          <div class="flex items-center gap-2">
            <ng-container
              [ngTemplateOutlet]="itemContentTemplate()"
              [ngTemplateOutletContext]="{ item }"
            />
          </div>
        </ng-template>
      }
    </p-select>
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
export class DropdownInputComponent<T> {
  protected readonly _id = `tb-${crypto.randomUUID()}`;
  hcd = inject(HostControlDirective);
  data = input<T[]>([]);
  textField = input<string | undefined>(undefined);
  valueField = input<string | undefined>(undefined);
  placeholder = input<string>(null!);
  value = model<T>();
  valueChange = output<T>();
  label = input<string>();

  clearable = input<boolean>(false);
  allowAdd = input(false);

  itemContentTemplate = contentChild<TemplateRef<any>>('itemContentTemplate');

  ngOnInit() {
    if (this.hcd?.control?.value) {
      this.value.set(this.hcd.control.value);
    }
  }

  onChange(event: SelectChangeEvent) {
    this.valueChange.emit(event.value);
  }

  clear() {
    this.value.set(null as any);
    this.valueChange.emit(null as any);
    this.hcd.control.setValue(null);
  }

  renderValue = fieldValue;
}
