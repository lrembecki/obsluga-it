/* eslint-disable no-unused-vars */
import { Signal } from '@angular/core';

export type SortDirection = 'asc' | 'desc';

export interface TableSort {
    column: string;
    direction: SortDirection;
}

export interface TableFeatures {
    editable?: boolean;
    quicksearch?: boolean;
    sortable?: boolean;
    reorderable?: boolean;
}

export interface TableColumnConfig<T> {
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

export interface DataTableInputs<T> {
    data: Signal<T[]>;
    columns: TableColumnConfig<T>[];
    features?: TableFeatures;
    rowIdField?: keyof T & string;
    loading?: boolean | Signal<boolean>;
    saving?: boolean | Signal<boolean>;
    deleting?: boolean | Signal<boolean>;
    searchPlaceholder?: string;
}
