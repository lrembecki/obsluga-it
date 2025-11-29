import { CommonModule } from '@angular/common';
import { Component, computed, input } from '@angular/core';
import {
  FormArray,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';
import { fieldValue } from '@app/core/helpers/field.helper';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DatePickerModule } from 'primeng/datepicker';
import { InputText } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import {
  CollectionFormFieldSchema,
  FormFieldSchema,
} from '../form-schema.model';
import { CheckboxInput } from './checkbox-input';
import { CustomInput } from './custom-input';
import { DateInput } from './date-input';
import { ImageInput } from './image-input';
import { SelectInput } from './select-input';
import { TextInput } from './text-input';
import { TextareaInput } from './textarea-input';

@Component({
  selector: 'app-collection-input',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ButtonModule,
    InputText,
    TextareaModule,
    CheckboxModule,
    DatePickerModule,
    TextInput,
    TextareaInput,
    SelectInput,
    DateInput,
    ImageInput,
    CheckboxInput,
    CustomInput,
  ],
  template: `
    <h2>{{ collectionField().label }}</h2>

    <div class="collection">
      @if (length() === 0) {
        <div class="empty">
          {{ collectionField().emptyText || 'Brak elementów' }}
        </div>
      }

      @for (i of indexes(); track i) {
        <div class="item">
          @if (isPrimitive()) {
            @switch (collectionField().itemType) {
              @case ('text') {
                <input
                  pInputText
                  [placeholder]="collectionField().placeholder"
                  [formControl]="itemControl(i)"
                />
              }
              @case ('textarea') {
                <textarea
                  pTextarea
                  class="w-full"
                  [formControl]="itemControl(i)"
                ></textarea>
              }
              @case ('checkbox') {
                <p-checkbox binary="true" [formControl]="itemControl(i)" />
              }
              @case ('date') {
                <p-datepicker [formControl]="itemControl(i)" />
              }
              @default {
                <input pInputText [formControl]="itemControl(i)" />
              }
            }
          } @else {
            <div class="item-grid">
              @for (sub of collectionField().itemFields ?? []; track sub.key) {
                @if (sub.isVisible) {
                  <div
                    class="sub-field"
                    [ngClass]="sub.colClass || collectionField().itemColClass"
                  >
                    @if (sub.type === 'text') {
                      <app-text-input [field]="sub" [form]="itemGroup(i)" />
                    }
                    @if (sub.type === 'textarea') {
                      <app-textarea-input [field]="sub" [form]="itemGroup(i)" />
                    }
                    @if (sub.type === 'select') {
                      <app-select-input [field]="sub" [form]="itemGroup(i)" />
                    }
                    @if (sub.type === 'date') {
                      <app-date-input [field]="sub" [form]="itemGroup(i)" />
                    }
                    @if (sub.type === 'checkbox') {
                      <app-checkbox-input [field]="sub" [form]="itemGroup(i)" />
                    }
                    @if (sub.type === 'image') {
                      <app-image-input [field]="sub" [form]="itemGroup(i)" />
                    }
                    @if (sub.type === 'custom') {
                      <app-custom-input [field]="sub" [form]="itemGroup(i)" />
                    }
                  </div>
                }
              }
            </div>
          }

          <div class="actions">
            <button
              pButton
              icon="pi pi-arrow-up"
              [disabled]="i === 0"
              (click)="move(i, -1)"
              text
            ></button>
            <button
              pButton
              icon="pi pi-arrow-down"
              [disabled]="i === lastIndex()"
              (click)="move(i, 1)"
              text
            ></button>
            <button
              pButton
              icon="pi pi-trash"
              severity="danger"
              (click)="remove(i)"
              text
            ></button>
          </div>
        </div>
      }

      <div class="add-row">
        <button
          pButton
          icon="pi pi-plus"
          [label]="collectionField().addButtonText || 'Dodaj'"
          (click)="add()"
          [disabled]="maxReached()"
        ></button>
      </div>
    </div>
  `,
  styles: `
    :host {
      display: flex;
      flex-direction: column;
      gap: 0.25rem;
    }
    .collection {
      display: flex;
      flex-direction: column;
      gap: 0.5rem;
    }
    .item {
      display: flex;
      align-items: start;
      gap: 0.5rem;
    }
    .actions {
      display: flex;
      gap: 0.25rem;
      margin-left: auto;
    }
    .item-grid {
      flex: auto;
      display: flex;
      gap: 0.5rem;
    }
    .sub-field {
      flex: auto;
      display: block;
    }
    .add-row {
      margin-top: 0.25rem;
    }
    .empty {
      color: var(--muted);
      font-style: italic;
    }
  `,
})
export class CollectionInput {
  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();

  protected collectionField = computed(
    () => this.field() as CollectionFormFieldSchema<any, any>,
  );
  protected get formArrayCtrl(): FormArray {
    return this.form().get(this.collectionField().key as string) as FormArray;
  }

  protected isPrimitive = computed(
    () =>
      !!this.collectionField().itemType && !this.collectionField().itemFields,
  );

  length(): number {
    return this.formArrayCtrl?.length ?? 0;
  }
  lastIndex(): number {
    return Math.max(0, this.length() - 1);
  }
  indexes(): number[] {
    return Array.from({ length: this.length() }, (_, i) => i);
  }

  itemGroup(index: number): FormGroup {
    return this.formArrayCtrl.at(index) as FormGroup;
  }

  itemControl(index: number): FormControl {
    return this.formArrayCtrl.at(index) as FormControl;
  }

  add(): void {
    const arr = this.formArrayCtrl;
    if (this.maxReached()) return;

    if (this.isPrimitive()) {
      arr.push(new FormControl(null));
    } else {
      arr.push(this.buildItemGroup());
    }

    this.recalculateOrder();
  }

  remove(index: number): void {
    const min = this.collectionField().minItems ?? 0;
    if (this.length() <= min) return;
    this.formArrayCtrl.removeAt(index);
    this.recalculateOrder();
  }

  move(index: number, delta: number): void {
    const arr = this.formArrayCtrl;
    const newIndex = index + delta;
    if (newIndex < 0 || newIndex >= arr.length) return;

    const ctrl = arr.at(index);
    arr.removeAt(index);
    arr.insert(newIndex, ctrl);
    this.recalculateOrder();
  }

  maxReached(): boolean {
    const max = this.collectionField().maxItems ?? Number.POSITIVE_INFINITY;
    return this.length() >= max;
  }

  private buildItemGroup(): FormGroup {
    const fields = this.collectionField().itemFields ?? [];
    const groupControls: Record<string, any> = {};
    fields.forEach((f) => {
      groupControls[f.key as string] = f.createControl('create', !!f.disabled);
    });
    return new FormGroup(groupControls);
  }

  private recalculateOrder(): void {
    const orderKey = this.collectionField().orderField as string | undefined;
    if (!orderKey || this.isPrimitive()) return;

    const arr = this.formArrayCtrl;
    for (let i = 0; i < arr.length; i++) {
      const grp = arr.at(i) as FormGroup;
      const ctrl = grp.get(orderKey);
      if (ctrl) ctrl.setValue(i + 1, { emitEvent: false });
    }
  }

  protected renderValue = fieldValue;
}
