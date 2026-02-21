import { EnvironmentProviders, inject, Provider, signal } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { facadeScope } from '@obsluga-it/core/facades';
import { FormSchema } from './form-field.schema';
import { Subscription } from 'rxjs';
import {
  ArrayFormTemplateModelProvider,
  FormTemplateModelProvider,
} from './form-model.provider';

export function provideFormService(
  provider: any,
): (Provider | EnvironmentProviders)[] {
  return [
    provider,
    { provide: ArrayFormService, useExisting: provider },
    {
      provide: FormTemplateModelProvider,
      useExisting: ArrayFormTemplateModelProvider,
    },
    ArrayFormTemplateModelProvider,
  ];
}

export function createFormRoute(provider: any): any {
  return {
    providers: [provideFormService(provider)],
    path: ':id',
    loadComponent: () =>
      import('./form-template').then((m) => m.FormTemplate),
  };
}

export abstract class ArrayFormService<T> {
  private readonly _modelProvider = inject(FormTemplateModelProvider<T>);
  protected readonly _schema = signal<FormSchema<T>>(null!);
  protected readonly _returnRoute = signal<string[]>(['../list']);
  public readonly _onChangeSubscriptions = new Map<string, Subscription>();

  public readonly schema = this._schema.asReadonly();
  public readonly returnRoute = this._returnRoute.asReadonly();
  public readonly facade = inject(facadeScope);
  public readonly model = this._modelProvider.model.asReadonly();
  public readonly mode = this._modelProvider.mode.asReadonly();
  public readonly isLoading = this.facade.loading;

  public buildFormControls(formGroup: FormGroup, model: any) {
    (this.schema()?.fields || []).forEach((field: any) => {
      if (field.type === 'collection') {
        const value = model?.[field.key as string] ?? ([] as any[]);
        const fa = formGroup.get(field.key as string) as any;
        if (fa && Array.isArray(value)) {
          while (fa.length > 0) fa.removeAt(0);
          value.forEach((item: any) => {
            if (field.itemFields?.length) {
              const groupControls: Record<string, any> = {};
              field.itemFields.forEach((f: any) => {
                const ctrl = f.createControl('edit', !!f.disabled);
                groupControls[f.key as string] = ctrl;
              });
              const fg = new FormGroup(groupControls);
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
