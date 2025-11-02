import { Injectable, signal } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class BreadcrumbsService {
  private readonly _navigations = signal<readonly MenuItem[]>([]);
  public readonly data = this._navigations.asReadonly();

  public udpate(input: MenuItem[]): void {
    this._navigations.set(input);
  }

  public add(input: MenuItem) {
    this._navigations.set([...this._navigations(), input]);
  }

  public remove(input: MenuItem): void {
    this._navigations.set([...this._navigations().filter((e) => e !== input)]);
  }
}
