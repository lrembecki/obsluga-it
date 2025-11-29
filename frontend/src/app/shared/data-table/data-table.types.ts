/* eslint-disable no-unused-vars */

import { InjectionToken } from '@angular/core';
import { ApiFacade } from '@app/core/interfaces/facade.interface';

// DI tokens to allow configuring DataTable via dependency injection
export const DataTableSchemaScope = new InjectionToken<DataTableSchema<any>>(
  'DataTableSchemaScope',
);
export const DataTableFacadeScope = new InjectionToken<ApiFacade<any>>(
  'DataTableFacadeScope',
);

export type SortDirection = 'asc' | 'desc';

export interface TableSort {
  column: string;
  direction: SortDirection;
}

export interface DataTableSchema<T> {
  editable?: boolean;
  quicksearch?: boolean;
  sortable?: boolean;
  reorderable?: boolean;
  columns: DataTableColumnSchema<T>[];
  persistenceKey?: string;
}

export interface DataTableColumnSchema<T> {
  field: keyof T & string;
  label: string;
  type?: 'text' | 'number' | 'date' | 'dropdown' | 'custom';
  editable?: boolean;
  sortable?: boolean;
  width?: string;
  render?: (row: T) => string;
  renderLink?: (row: T) => string[];
  options?: Array<{ label: string; value: unknown }>;
}
