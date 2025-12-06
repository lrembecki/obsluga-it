import { CommonModule } from '@angular/common';
import {
  Component,
  computed,
  CUSTOM_ELEMENTS_SCHEMA,
  input,
} from '@angular/core';
import {
  FormArray,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
} from '@angular/forms';
import { fieldValue } from '@app/core/helpers/field.helper';
import { CollectionFormFieldSchema, FormFieldSchema } from '@app/shared/forms';
import { AccordionModule } from 'primeng/accordion';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { DatePickerModule } from 'primeng/datepicker';
import { TextareaModule } from 'primeng/textarea';
import { RenderInput } from './render-input';

@Component({
  selector: 'app-collection-input',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ButtonModule,
    TextareaModule,
    AccordionModule,
    CheckboxModule,
    DatePickerModule,
    RenderInput,
  ],
  template: `
    <p-accordion value="0">
      <p-accordion-panel value="0">
        <p-accordion-header>
          <div
            style="display: flex; align-items: center; gap: 0.5rem; width: 100%; margin-right: 1rem;"
          >
            <span>{{ collectionField().label }}</span>
            <button
              pButton
              icon="pi pi-plus"
              (click)="add(); $event.stopPropagation()"
              [disabled]="maxReached()"
              size="small"
              style="margin-left: auto;"
            ></button>
          </div>
        </p-accordion-header>
        <p-accordion-content>
          <div
            class="collection"
            [class.horizontal]="layout() === 'horizontal'"
          >
            @if (length() === 0) {
              <div class="empty">
                {{ collectionField().emptyText || 'Brak elementów' }}
              </div>
            }

            @for (i of indexes(); track i) {
              <div class="item">
                <div class="item-grid">
                  @for (
                    sub of collectionField().itemFields ?? [];
                    track sub.key
                  ) {
                    @if (sub.isVisible) {
                      <div
                        class="sub-field"
                        [ngClass]="
                          sub.colClass || collectionField().itemColClass
                        "
                      >
                        <app-render-input [field]="sub" [form]="itemGroup(i)" />
                      </div>
                    }
                  }
                </div>

                <div class="actions">
                  <button
                    pButton
                    [icon]="
                      layout() === 'horizontal'
                        ? 'pi pi-arrow-left'
                        : 'pi pi-arrow-up'
                    "
                    [disabled]="i === 0"
                    (click)="move(i, -1)"
                    text
                  ></button>
                  <button
                    pButton
                    [icon]="
                      layout() === 'horizontal'
                        ? 'pi pi-arrow-right'
                        : 'pi pi-arrow-down'
                    "
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
          </div>
        </p-accordion-content>
      </p-accordion-panel>
    </p-accordion>
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
    .collection.horizontal {
      flex-direction: row;
      flex-wrap: wrap;
      align-items: stretch;
    }
    .item {
      display: flex;
      align-items: start;
      gap: 0.5rem;
    }
    .collection.horizontal .item {
      flex-direction: column;
      align-items: stretch;
      border: 1px solid var(--border);
      padding: 0.5rem;
      border-radius: 0.25rem;
      min-width: 14rem;
    }
    .actions {
      display: flex;
      gap: 0.25rem;
      margin-left: auto;
      margin-top: 1.5rem;
      margin-bottom: auto;
    }
    .collection.horizontal .actions {
      margin-left: 0;
      margin-top: 0.5rem;
      justify-content: flex-end;
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
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class CollectionInput {
  field = input.required<FormFieldSchema<unknown>>();
  form = input.required<FormGroup>();

  protected collectionField = computed(
    () => this.field() as any as CollectionFormFieldSchema<any, any>,
  );
  layout = computed(() => this.collectionField().layout);
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
