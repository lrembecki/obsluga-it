import { CommonModule } from '@angular/common';
import { Component, inject, input, model, OnInit, output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ChipModule } from 'primeng/chip';
import { HostControlDirective } from './host-control.directive';

@Component({
  selector: 'app-combobox-input',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AutoCompleteModule,
    ChipModule,
  ],
  template: `
    <div class="combobox-container">
      <p-autoComplete
        [formControl]="hcd.control"
        [suggestions]="filteredOptions"
        [placeholder]="placeholder()"
        [dropdown]="true"
        [forceSelection]="false"
        [completeOnFocus]="true"
        [multiple]="true"
        (completeMethod)="filterOptions($event)"
        (onSelect)="onSelect($event)"
        (onKeyDown)="onKeyDown($event)"
        styleClass="w-full"
      />

      <!-- Display for multiple selection mode -->
      <div
        *ngIf="multiple() && selectedItems.length > 0"
        class="chips-container"
      >
        <p-chip
          *ngFor="let item of selectedItems"
          [label]="item"
          [removable]="true"
          (onRemove)="removeItem(item)"
        />
      </div>

      <!-- Display for single selection mode -->
      <div *ngIf="!multiple() && selectedValue" class="single-value">
        <span>{{ selectedValue }}</span>
        <button type="button" class="clear-button" (click)="clearSelection()">
          ×
        </button>
      </div>
    </div>
  `,
  styles: `
    .combobox-container {
      width: 100%;
    }

    :host ::ng-deep .p-autocomplete {
      width: 100%;
    }

    :host ::ng-deep .p-autocomplete .p-inputtext {
      width: 100%;
    }

    .chips-container {
      margin-top: 8px;
      display: flex;
      flex-wrap: wrap;
      gap: 4px;
    }

    .single-value {
      margin-top: 8px;
      display: flex;
      align-items: center;
      justify-content: space-between;
      padding: 4px 8px;
      background-color: #f8f9fa;
      border-radius: 4px;
      border: 1px solid #dee2e6;
    }

    .clear-button {
      background: none;
      border: none;
      font-size: 16px;
      cursor: pointer;
      color: #6c757d;
      padding: 0 4px;
      line-height: 1;
    }

    .clear-button:hover {
      color: #dc3545;
    }
  `,
  hostDirectives: [HostControlDirective],
})
export class ComboboxInput implements OnInit {
  protected readonly hcd = inject(HostControlDirective);
  public readonly options = input<string[]>([]);
  public readonly placeholder = input<string>('Wybierz lub wpisz...');
  public readonly multiple = input<boolean>(false);
  public value = model<string | string[]>('');
  public readonly optionsChange = output<string[]>();

  public readonly valueChange = output<string | string[] | undefined>();
  public readonly elementAdded = output<string>();
  public readonly elementRemoved = output<string>();

  public filteredOptions: string[] = [];
  public selectedItems: string[] = [];
  public selectedValue: string | null = null;

  public ngOnInit() {
    this.filteredOptions = [...this.options()];
    this.initializeValues();
  }

  private initializeValues() {
    const currentValue = this.value();
    if (this.multiple()) {
      this.selectedItems = Array.isArray(currentValue) ? currentValue : [];
    } else {
      this.selectedValue =
        typeof currentValue === 'string' ? currentValue : null;
    }
  }

  public filterOptions(event: any) {
    const query = event.query.toLowerCase();
    if (!query.trim()) {
      this.filteredOptions = [...this.options()];
    } else {
      this.filteredOptions = this.options().filter((option) =>
        option.toLowerCase().includes(query),
      );
    }
  }

  public onSelect(event: any) {
    if (this.multiple()) {
      if (!this.selectedItems.includes(event)) {
        this.selectedItems = [...this.selectedItems, event];
        this.value.set(this.selectedItems);
        this.valueChange.emit(this.selectedItems);
        this.elementAdded.emit(event);
      }
    } else {
      this.selectedValue = event;
      this.value.set(event);
      this.valueChange.emit(event);
      this.elementAdded.emit(event);
    }
  }

  public onKeyDown(event: any) {
    console.log({ event });

    if (event.key === 'Enter') {
      const currentValue = this.hcd.control.value;
      if (
        currentValue &&
        currentValue.trim() &&
        !this.options().includes(currentValue.trim())
      ) {
        this.addNewOption(currentValue.trim());
      }
    }
  }

  public addNewOption(newOption: string) {
    if (!this.options().includes(newOption)) {
      const newOptions = [...this.options(), newOption];
      this.optionsChange.emit(newOptions);
    }

    if (this.multiple()) {
      if (!this.selectedItems.includes(newOption)) {
        this.selectedItems = [...this.selectedItems, newOption];
        this.value.set(this.selectedItems);
        this.valueChange.emit(this.selectedItems);
        this.elementAdded.emit(newOption);
      }
    } else {
      this.selectedValue = newOption;
      this.value.set(newOption);
      this.valueChange.emit(newOption);
      this.elementAdded.emit(newOption);
    }
  }

  public removeItem(item: string) {
    if (this.multiple()) {
      this.selectedItems = this.selectedItems.filter(
        (selected) => selected !== item,
      );
      this.value.set(this.selectedItems);
      this.valueChange.emit(this.selectedItems);
      this.elementRemoved.emit(item);
    }
  }

  public clearSelection() {
    if (!this.multiple()) {
      const removedValue = this.selectedValue;
      this.selectedValue = null;
      this.value.set('');
      this.valueChange.emit(undefined);
      if (removedValue) {
        this.elementRemoved.emit(removedValue);
      }
    }
  }
}
