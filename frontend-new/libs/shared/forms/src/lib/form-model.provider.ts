import { Injectable, signal } from '@angular/core';

@Injectable()
export class FormTemplateModelProvider<T = any> {
  public readonly id = signal<string | null>(null);
  public readonly mode = signal<'create' | 'edit'>('create');
  public readonly model = signal<T | null>(null);
}

@Injectable()
export class ArrayFormTemplateModelProvider<T = any> extends FormTemplateModelProvider<T> {}
