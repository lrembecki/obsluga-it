# DataTable Component

A signals-first, standalone data table with optional inline editing, quicksearch, column sorting, and header drag-and-drop reordering.

## Import
```ts
import { DataTable } from 'app/shared/ui/data-table/data-table';
```

## Inputs
- `data: Signal<T[]>` — required; facade data signal.
- `columns: TableColumnConfig<T>[]` — column definitions.
- `features?: TableFeatures` — `{ editable?, quicksearch?, sortable?, reorderable? }`.
- `rowIdField?: keyof T & string` — defaults to `'id'`.
- `loading?: boolean | Signal<boolean>` — optional.
- `saving?: boolean | Signal<boolean>` — optional.
- `deleting?: boolean | Signal<boolean>` — optional.
- `searchPlaceholder?: string` — pass an i18n string (e.g., `'DATA_TABLE.SEARCH_PLACEHOLDER' | translate` from parent) or raw text.
- `actionsLabel?: string` — label for actions header.
- `editLabel?: string` — edit button text.
- `saveLabel?: string` — save button text.
- `cancelLabel?: string` — cancel button text.
 - `persistenceKey?: string` — scopes column order persistence per table instance (default: `'default'`).

## Outputs
- `save: T` — emitted on inline save; call `facade.update(id, dtoOrVm)`.
- `orderBy: TableSort` — emitted when sorting headers clicked.
- `columnsChange: TableColumnConfig<T>[]` — emitted after header reordering.
- `searchQuery: string` — emitted on quicksearch input changes.

## Basic Usage
```html
<app-data-table
  [data]="facade.data"
  [columns]="columns"
  [features]="{ editable: true, quicksearch: true, sortable: true, reorderable: true }"
  [searchPlaceholder]="('DATA_TABLE.SEARCH_PLACEHOLDER' | translate)"
  [actionsLabel]="('DATA_TABLE.ACTIONS' | translate)"
  [editLabel]="('DATA_TABLE.EDIT' | translate)"
  [saveLabel]="('DATA_TABLE.SAVE' | translate)"
  [cancelLabel]="('DATA_TABLE.CANCEL' | translate)"
  [persistenceKey]="'permissions-table'"
  (save)="onSave($event)"
  (orderBy)="onOrderBy($event)"
  (columnsChange)="onColumnsChange($event)"
></app-data-table>
```

## Column Config
```ts
export interface TableColumnConfig<T> {
  field: keyof T & string;
  label: string;
  type?: 'text' | 'number' | 'date' | 'dropdown' | 'custom';
  editable?: boolean;
  sortable?: boolean;
  width?: string;
  render?: (row: T) => string; // for custom rendering
  options?: Array<{ label: string; value: unknown }>;
}
```

## Notes
- The component does not call APIs; use facade methods in your handlers.
- Column order persists in `localStorage` under `data-table:columns:<persistenceKey>:<rowIdField>`.
- For large datasets, prefer facade-side search/sort; the table emits events for integration.

## Testing (CI)
- Prefer `ChromeHeadless` in Karma for CI. If using Chrome on Windows, set `CHROME_BIN` to the installed path.
  - Example PowerShell:
    ```powershell
    $env:CHROME_BIN = "C:\Program Files\Google\Chrome\Application\chrome.exe"
    npm run test
    ```
  - Or adjust `karma.conf.js` to use `browsers: ['ChromeHeadless']`.
