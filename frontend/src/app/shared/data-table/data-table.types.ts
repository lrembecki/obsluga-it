/* eslint-disable no-unused-vars */

import { InjectionToken } from '@angular/core';
import { ArrayApiFacade } from '@app/core/interfaces/facade.interface';
import { FormSchema } from '../forms';

// DI tokens to allow configuring DataTable via dependency injection
export const DataTableSchemaScope = new InjectionToken<DataTableSchema<any>>(
  'DataTableSchemaScope',
);
export const DataTableFacadeScope = new InjectionToken<ArrayApiFacade<any>>(
  'DataTableFacadeScope',
);

export type SortDirection = 'asc' | 'desc';

export interface TableSort {
  column: string;
  direction: SortDirection;
}

export interface DataTableSchema<T> {
  filter?: FormSchema<any>;
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
  type?:
    | 'text'
    | 'number'
    | 'date'
    | 'dropdown'
    | 'boolean'
    | 'custom'
    | 'date-time';
  editable?: boolean;
  sortable?: boolean;
  width?: string;
  render?: (row: T) => string;
  renderLink?: (row: T) => string[] | { url: string };
  options?: Array<{ label: string; value: unknown }>;
}
