import { Injectable, signal } from '@angular/core';

export interface BreadcrumbItem {
  label: string;
  url?: string;
}

@Injectable({ providedIn: 'root' })
export class BreadcrumbsService {
  private readonly _navigations = signal<readonly BreadcrumbItem[]>([]);
  public readonly data = this._navigations.asReadonly();

  public update(input: BreadcrumbItem[]): void {
    this._navigations.set(input);
  }

  public add(input: BreadcrumbItem): void {
    this._navigations.set([...this._navigations(), input]);
  }

  public remove(input: BreadcrumbItem): void {
    this._navigations.set([...this._navigations().filter((e) => e !== input)]);
  }
}
