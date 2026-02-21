import { InjectionToken } from '@angular/core';

export const DataTableSchemaScope = new InjectionToken<DataTableSchema<any>>(
  'DataTableSchemaScope',
);

export type SortDirection = 'asc' | 'desc';

export interface TableSort {
  column: string;
  direction: SortDirection;
}

export interface DataTableSchema<T> {
  filter?: any;
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
  type?: 'text' | 'number' | 'date' | 'dropdown' | 'boolean' | 'custom' | 'date-time';
  editable?: boolean;
  sortable?: boolean;
  width?: string;
  render?: (row: T) => string;
  renderLink?: (row: T) => string[] | { url: string };
  options?: Array<{ label: string; value: unknown }>;
}
