# Corporate Data-Table: Implementation & Migration Plan

## Goals
- Unified, reusable data-table with feature toggles: inline editing, quicksearch, sort by column, and column reordering.
- Signals-first architecture; table consumes `Signal<T[]>` from facades and exposes minimal outputs for mutations.
- Strict separation of responsibilities: UI in table; data fetching/mutations via facades; no direct API calls in components.
- Accessibility, performance, theming, and i18n aligned with project guidelines.

## Scope
- New shared component: `src/app/shared/ui/data-table/` with standalone component(s) and helpers.
- Minimal additions to `core/helpers` for sorting/filtering utilities (if justified).
- No breaking changes to existing facades; introduce optional helpers to ease adoption.

---

## Architecture Overview

### Component Structure
```
src/app/shared/ui/data-table/
  data-table.ts              // Standalone component
  data-table.types.ts        // Column, sort, edit config interfaces
  data-table.state.ts        // Signals for search, sorting, editing
  cells/                     // (Optional) specialized cell editors (text/date/dropdown)
  data-table.spec.ts         // Minimal tests
```

### Key Interfaces (types)
- `TableColumnConfig<T>`: `{ field: keyof T; label: string; type?: 'text'|'number'|'date'|'dropdown'|'custom'; editable?: boolean; sortable?: boolean; width?: string; render?: (row: T) => string; options?: Array<{label:string;value:any}>; }`
- `TableSort`: `{ column: string; direction: 'asc'|'desc' }`
- `TableFeatures`: `{ editable?: boolean; quicksearch?: boolean; sortable?: boolean; reorderable?: boolean }`

### Public API (component inputs/outputs)
- Inputs:
  - `data: Signal<T[]>` (required)
  - `columns: TableColumnConfig<T>[]`
  - `features: TableFeatures`
  - `rowIdField: keyof T` (default: `id`)
  - `loading?: boolean | Signal<boolean>` (bind to facade.loading())
  - `saving?: boolean | Signal<boolean>` (bind to facade.saving())
  - `deleting?: boolean | Signal<boolean>`
  - `searchPlaceholder?: string` (i18n key)
- Outputs:
  - `save: (row: T) => void` — consumer calls `facade.update(row.id, dtoOrVm)`
  - `delete: (row: T) => void` — consumer calls `facade.delete(row.id)`
  - `orderBy: (sort: TableSort) => void` — consumer decides local sort vs facade filter
  - `columnsChange: (columns: TableColumnConfig<T>[])` — persist order (e.g., local storage)
  - `search: (query: string) => void` — consumer calls `facade.filter({ q: query })`

### Internal State (signals)
- `query = signal('')` with debounced derived signal for quicksearch.
- `sort = signal<TableSort | null>(null)`.
- `editRowId = signal<string | null>(null)`; `editDraft = signal<Partial<T> | null>(null)` for inline editing.
- `orderedColumns = signal<TableColumnConfig<T>[]>(columns)` for reordering.
- `visibleData = computed(() => applySearchSort(data(), query(), sort()))` — lightweight local transformations when facade-level is not used.

### UX/Template Patterns
- Use `@if`, `@for` control flow; `track row[rowIdField]`.
- Header row: sortable indicators, drag handles for reorderable columns.
- Quicksearch input above table; clear icon; debounce 250–400ms.
- Inline edit: row enters edit mode; cells render input components (`app-text-input`, `app-dropdown-input`, etc.) with `Valid` directive.
- Actions column: Save/Cancel in edit mode; Delete when not editing; Disable when `saving()`/`deleting()`.
- Accessibility: `role="grid"`, keyboard navigation, focus management when entering/exiting edit mode.

---

## Implementation Steps

### 1. Types & Contracts
- Create `data-table.types.ts` with `TableColumnConfig<T>`, `TableSort`, `TableFeatures`.
- Document constraints: `field` must map to a property in `T`; `render` used when type is `custom`.

### 2. Component Scaffold
- Add `data-table.ts` (standalone) with inputs/outputs and minimal template:
  - Panel with start slot: quicksearch when enabled.
  - Main table with columns defined from `columns` input.
  - Sortable headers (click toggles asc/desc/none).
  - Reorderable: drag-and-drop headers (HTML5 DnD) or directive.
  - Inline editors: conditional template rendering based on `features.editable` and `editRowId`.

### 3. Signals & Derived Data
- Implement `query`, `sort`, `orderedColumns`, `editRowId`, `editDraft` signals.
- Implement `visibleData = computed(...)` with pure functions:
  - `applySearch(data, query)` — searches across visible columns by default.
  - `applySort(data, sort)` — sorts by primitive/text/date with safe comparisons.
- Add small utility functions locally in component or `data-table.state.ts`.

### 4. Editing Flow
- Enter edit: set `editRowId(row.id)`; clone shallow row into `editDraft`.
- Bind `[(value)]` of editors to `editDraft[field]`.
- Save: emit `save(editDraft as T)`; guard while `saving` true; exit edit mode on `success` signaled by consumer or immediate exit with optimistic UI if desired.
- Cancel: reset `editDraft` and `editRowId`.
- Validation: wrap inputs in `<ng-container validate>`; disable Save if invalid.

### 5. Sorting & Reordering
- Sorting: click header cycles `asc → desc → none`; emit `orderBy(sort)` to consumer; apply local sort if consumer does not intercept.
- Reordering: drag header; update `orderedColumns.set(newOrder)`; emit `columnsChange(newOrder)`.
- Persist optional column order using existing storage helper or consumer-level persistence.

### 6. Quicksearch
- Input: uses shared input component; debounce via computed/throttled signal.
- Emit `search(query)` for facade-level filtering; fallback to local `applySearch` if consumer does not handle.

### 7. i18n & Theming
- Strings: `dataTable.search.placeholder`, `dataTable.actions.save`, `dataTable.actions.cancel`, `dataTable.actions.delete`.
- Add to `public/i18n/en.json` and `pl.json`.
- Colors: use design tokens; no hardcoded values; rely on `Button` severity and table CSS variables.

### 8. Accessibility
- `role="grid"`; headers use `button` semantics for sorting; provide `aria-sort` attribute.
- Drag handles have `aria-grabbed` / `aria-dropeffect` semantics; offer keyboard reordering later (future enhancement).
- Focus moves to first editable cell when entering edit mode; restore focus after save/cancel.

### 9. Testing (Minimal Baseline)
- Component tests:
  - Renders rows from `data()`.
  - Sorting toggles and reorders `visibleData`.
  - Quicksearch filters `visibleData`.
  - Inline edit binds and emits `save` with updated draft.
- Snapshot test for header reorder emission.

### 10. Performance Considerations
- Memoize search/sort via `computed()`; avoid recompute on unrelated signals.
- Avoid in-place mutations; always set new array references.
- Plan for virtual scroll in future for datasets > 1k rows.

---

## Facade Integration Pattern
- Bind: `[data]="facade.data"`, `[loading]="facade.loading()"`, `[saving]="facade.saving()"`.
- Save handler:
```ts
async onSave(row: T) {
  const id = (row as any).id;
  const result = await this.facade.update(id, row);
  if (result.success) { /* table exits edit mode via input change or event */ }
  // TODO: show toast on error when notification service is available
}
```
- Search handler:
```ts
onSearch(q: string) { this.facade.filter({ q }); }
```
- Sort handler (optional facade-level):
```ts
onOrderBy(sort: TableSort) { this.facade.filter({ sort }); }
```

---

## Migration Plan

### Inventory & Assessment
1. List current table usages under `src/app/shared/ui/ui-table` and feature folders.
2. Categorize by needed features: editing, sorting, quicksearch, reordering.
3. Identify custom columns/templates to port to `render` or to specialized cell components.

### Compatibility Shims
- Provide a thin wrapper component `ui-table` → `data-table` adapter to maintain selectors temporarily:
  - Map existing inputs to new `data-table` API.
  - Mark deprecated props with console warnings during development.

### Migration Steps per Feature
1. Replace import: `UiTable` → `DataTable` in target component.
2. Build `columns` config using existing column definitions; migrate custom templates to `render` or cell components.
3. Wire facade signals/flags: `[data]`, `[loading]`, `[saving]`.
4. Implement handlers: `(save)`, `(search)`, `(orderBy)`, `(columnsChange)`.
5. Remove local sorting/searching if facade-level filtering is adopted.
6. Validate inline edit flows (guard with `Valid`; disable submit when invalid).

### Routing & List Components
- List components continue to use provider injection pattern; only the table instance changes.
- Use absolute navigation for actions (unchanged).

### i18n Update
- Add keys once globally; remove ad-hoc strings in migrated components.

### Phased Rollout
- Phase 1: Migrate 1–2 low-risk lists (e.g., tags) to validate API.
- Phase 2: Migrate medium-complex lists (trip-requests).
- Phase 3: Migrate security-related tables (permissions/groups) with inline edits.
- Phase 4: Remove adapter, update `ui-table` docs to point to `data-table`.

### Risks & Mitigations
- Risk: Feature gaps vs. legacy implementations.
  - Mitigation: Keep adapter and feature toggles; prioritize parity features first.
- Risk: Performance regressions on large datasets.
  - Mitigation: Measure; add virtual scroll boundary later; offload sort/search to facade when needed.
- Risk: Custom column behavior lacking.
  - Mitigation: Support `type: 'custom'` with `render` and/or `<ng-template>` slots for advanced cases.

---

## Follow-ups & Enhancements
- Column visibility toggles; saved user preferences.
- Server-side pagination integration via facade filter protocol.
- Bulk edit mode and multi-select rows.
- Export (CSV/Excel) leveraging facade data.
- Virtual scroll for large datasets.

---

## Acceptance Criteria
- New `DataTable` component renders data via signals and supports: inline editing, quicksearch, sort by column, column reordering.
- At least one existing feature list migrated and functioning with parity.
- Tests cover editing, sorting, searching, and column reordering.
- i18n keys present in both `en.json` and `pl.json`.
- No direct API calls in table component; all mutations via facades.
