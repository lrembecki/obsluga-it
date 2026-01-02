import {
  computed,
  EnvironmentProviders,
  inject,
  linkedSignal,
  Provider,
  signal,
} from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { Route } from '@angular/router';
import { ApiFacade } from '@app/core/interfaces/facade.interface';
import { FormSchema } from '@app/shared/forms';
import { Subscription } from 'rxjs';

export function provideFormService(
  provider: any,
): (Provider | EnvironmentProviders)[] {
  return [provider, { provide: FormService, useExisting: provider }];
}

export function createFormRoute(provider: any): Route {
  return {
    providers: [provideFormService(provider)],
    path: ':id',
    loadComponent: () =>
      import('app/shared/forms/form-template').then((m) => m.FormTemplate),
  };
}

export abstract class FormService<T> {
  protected readonly _schema = signal<FormSchema<T>>(null!);
  protected readonly _returnRoute = signal<string[]>(['../list']);
  public readonly _onChangeSubscriptions = new Map<string, Subscription>();

  public readonly schema = this._schema.asReadonly();
  public readonly returnRoute = this._returnRoute.asReadonly();
  public readonly facade = inject(ApiFacade);
  public readonly id = signal<string>('create');
  public readonly model = linkedSignal(() =>
    this.facade.data().find((e) => e.id === this.id()),
  );
  public readonly isLoading = this.facade.loading;
  public readonly mode = computed(() =>
    this.id() === 'create' ? 'create' : 'edit',
  );
  public buildFormControls(formGroup: FormGroup, model: any) {
    // Populate FormArray controls for collection fields before patching values
    this.schema().fields.forEach((field: any) => {
      if (field.type === 'collection') {
        const value = model?.[field.key as string] ?? ([] as any[]);
        const fa = formGroup.get(field.key as string) as any;
        if (fa && Array.isArray(value)) {
          // Clear existing
          while (fa.length > 0) fa.removeAt(0);
          // Push item controls
          value.forEach((item: any) => {
            if (field.itemFields?.length) {
              const groupControls: Record<string, any> = {};
              field.itemFields.forEach((f: any) => {
                const ctrl = f.createControl('edit', !!f.disabled);
                groupControls[f.key as string] = ctrl;
              });
              const fg = new (formGroup.constructor as any)(groupControls);
              fa.push(fg);
            } else {
              fa.push(new FormControl(item));
            }
          });
        }
      }
    });
    return model;
  }

  public registerOnChangeHandlers(
    schema: FormSchema<T>,
    formGroup: FormGroup,
  ): void {
    schema.fields.forEach((field) => {
      if (!field.onChange) return;
      const control = formGroup.get(field.key as string);
      if (!control) return;

      const root = this.getRootFormGroup(control);
      if (!root) return;

      const triggerOnChange = () => {
        field.onChange!(control, formGroup);
        field.root.onChange(root);
      };

      const subscription = control.valueChanges.subscribe(() =>
        queueMicrotask(triggerOnChange),
      );
      this._onChangeSubscriptions.set(field.key as string, subscription);

      queueMicrotask(triggerOnChange);
    });
  }

  public getRootFormGroup(control: AbstractControl): FormGroup | null {
    let parent = control.parent;
    while (parent?.parent) {
      parent = parent.parent;
    }
    return parent as FormGroup | null;
  }

  public cleanupOnChangeSubscriptions(): void {
    this._onChangeSubscriptions.forEach((sub) => sub.unsubscribe());
    this._onChangeSubscriptions.clear();
  }

  initialize(): Promise<void> {
    return this.facade.initialize();
  }
}
