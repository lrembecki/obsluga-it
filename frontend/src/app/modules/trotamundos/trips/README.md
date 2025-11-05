# Trotamundos Trips Feature

Implements CRUD UI for Trips using endpoint `trotamundos/trips`.

Files:
- `trip.vm.ts` – View model used in UI (fields: id, name, isActive, isDisabled, title, subtitle, description).
- `trip.dto.ts` – Transport DTO & mapping helpers.
- `trip.provider.ts` – Facade + provider injection wrapping API calls, sorting by name then title then subtitle.
- `trip-list.ts` – List component with navigation to create/edit and columns (Name, Title, Subtitle, Description, Active, Disabled).
- `trip-form.ts` – Form component using cachedComputed for dirty edit session; includes checkbox toggles for Active/Disabled and Name maxlength=250.
- `routes.ts` – Lazy list/create/edit routing via shared `listRoute` helper.
- `trip.provider.spec.ts` – Sorting logic test.
- `routes.spec.ts` – Basic route shape test.

Add permission key: `Trotamundos.Trips`.
Add i18n keys: `TROTAMUNDOS.TRIPS` (en/pl).

Backend expectation:
- GET `trotamundos/trips` returns `TripVM[]` (including new fields).
- POST `trotamundos/trips` accepts `TripDTO` (name, isActive, isDisabled, title, subtitle, description) returns updated `TripVM[]`.
- PUT `trotamundos/trips/{id}` updates trip.
- DELETE `trotamundos/trips/{id}` deletes trip.
