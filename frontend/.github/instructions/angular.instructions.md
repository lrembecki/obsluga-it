---
applyTo: '**'
---
## Angular Project Feature Creation & Coding Guidelines

This document defines how to add NEW FEATURES (domain slices) to the project, based on the existing `permissions` example located at `src/app/modules/security/permissions`. Follow these rules whenever generating code, reviewing changes, or answering architecture questions. Keep changes consistent, minimal, and aligned with established patterns.

---
### 1. Core Principles
1. Standalone components only (no NgModules for new code) – use the `imports` array in `@Component`.
2. Keep features SMALL: co-locate components, providers, routes, and the view model in a single folder.
3. Favor Signals (`computed`, `toSignal`) for reactive state derivation; avoid manual subscriptions unless required.
4. Use Facade pattern to wrap backend API calls; never call `ApiService` directly from components.
5. Keep UI logic declarative: templates should reflect state pulled from facades/providers.
6. Prefer immutability (create new objects instead of mutating arrays in-place) inside facades when transforming data.

---
### 2. When to Create a Feature Folder
Create a new feature folder when you introduce a cohesive CRUD or list/detail workflow (e.g., permissions, tags, trip-requests). If the domain logically belongs to an existing top-level area:
- Use `src/app/features/<domain>` for general application entities.
- Use `src/app/modules/<module>/<domain>` for module-scoped entities (e.g., security-related under `modules/security`).
Avoid deep nesting beyond: `modules/<module>/<feature>`.

---
### 3. Standard Folder Contents (Example: `permissions`)
Each feature folder MUST contain:
| File | Purpose |
|------|---------|
| `<entity>.vm.ts` | View Model class defining shape used in UI + constructor for partial init |
| `<entity>-list.ts` | List component (displays table/list, navigation to create/edit) |
| `<entity>-form.ts` | Form component (create/update + delete panel) |
| `<entity>.provider.ts` | Facade + provider/inject helper + provider type alias |
| `routes.ts` | Route configuration using `listRoute` helper |
Optional additions: `*.spec.ts` tests, `*.mocks.ts`, `*.constants.ts` if complexity grows.

File naming: hyphen-case for files; PascalCase for exported classes. For plural resources, keep folder plural (e.g. `permissions`) and use singular for the VM (`PermissionVM`).

---
### 4. View Model (`<entity>.vm.ts`)
Pattern:
```ts
export class PermissionVM { // Replace Permission with Entity name
	id: string = null!;
	name: string = null!; // Add domain fields
	constructor(init?: Partial<PermissionVM>) { Object.assign(this, init); }
}
```
Rules:
- Always include an `id` property (string, nullable via `null!` initial assignment) unless truly not needed.
- Initialize via `constructor(init?: Partial<Model>) { Object.assign(this, init); }` for ergonomic hydration.
- Keep it POJO-like; no Angular decorators, no heavy logic. Derived values belong in components/facade `computed` signals.

---
### 5. Facade & Provider (`<entity>.provider.ts`)
Responsibilities:
- Fetch, cache, and expose data via inherited API facade methods.
- Perform lightweight transformation/sorting inside `withData()`.
- Expose Angular services (Router, ActivatedRoute, FormBuilder) through a typed provider object.

Pattern:
```ts
export class SecurityPermissionFacade extends ApiFacade<PermissionVM[]> {
	constructor() { super([], 'account/permissions'); } // endpoint path
	protected override withData(data: PermissionVM[]): PermissionVM[] {
		return data.sort((a, b) => a.name.localeCompare(b.name));
	}
}

export function provideSecurityPermissions() { return [SecurityPermissionFacade]; }
export function injectSecurityPermissions(): SecurityPermissionProvider { return {
	permissions: inject(SecurityPermissionFacade),
	router: inject(Router),
	formBuilder: inject(FormBuilder),
	activatedRoute: inject(ActivatedRoute)
}; }

export type SecurityPermissionProvider = {
	permissions: SecurityPermissionFacade;
	router: Router;
	formBuilder: FormBuilder;
	activatedRoute: ActivatedRoute;
};
```
Rules:
- Facade name: `<Context><Entity>Facade` (e.g. `SecurityPermissionFacade`). Context prefix optional unless multiple domains would collide.
- Endpoint passed to `super([], 'path')` uses backend REST path; change only here when adding new resource.
- Provide an inject helper returning a fully typed provider object.

When adding NEW feature: replicate pattern, adapt names and endpoint segment.

---
### 6. Routing (`routes.ts`)
Use `listRoute<Model, ProviderType>( providers, findFn, injectFn, listLoader, formLoader )` helper.
Pattern:
```ts
export const routes: Routes = [
	listRoute<PermissionVM, SecurityPermissionProvider>(
		provideSecurityPermissions(),
		(id, services) => services.permissions.data().find(e => e.id === id)!,
		injectSecurityPermissions,
		() => import('./permission-list').then(m => m.PermissionList),
		() => import('./permission-form').then(m => m.PermissionForm)
	)
];
```
Rules:
- Always lazy-load components via dynamic imports for code-splitting.
- The lookup function MUST be null-safe; if data may be absent, handle gracefully (e.g., remove non-null assertion and navigate back or show 404 state).
- Keep `routes` exported as a top-level constant.

---
### 7. List Component (`<entity>-list.ts`)
Purpose: Display collection, navigate to create/edit.
Key conventions:
- Use injected provider: `protected readonly _services = inject<...>()`.
- Template uses UI primitives (`app-ui-table`, `app-ui-panel`, `app-button`).
- Provide a `renderLink(record)` returning relative router link parts.

Minimal skeleton:
```ts
@Component({
	selector: 'app-permission-list',
	imports: [UiPanel, UiTable, UiTableColumn, RouterLink, UiTableColumnLink, Button],
	template: `
		<app-ui-panel>
			<ng-template>
				<app-button color="primary" text="Create" routerLink="../create" />
			</ng-template>
		</app-ui-panel>
		<app-ui-table [data]="_services.permissions.data()">
			<app-ui-table-column text="Name" field="name" link [renderLink]="renderLink" />
		</app-ui-table>
	`
})
export class PermissionList {
	protected readonly _services = injectSecurityPermissions();
	protected renderLink = (record: PermissionVM) => ['..', record.id];
}
```

---
### 8. Form Component (`<entity>-form.ts`)
Responsibilities:
- Create or update existing record.
- Delete existing record (only if `id` present).
- Handle validation via `Valid` + `Required` directives and host directives.

Patterns from example:
- Use `toSignal(route.params)` for reactive param extraction.
- Derive `permissionId` via `computed`.
- Derive `model` via `computed`, falling back to `new ModelVM()`.
- Re-use facade methods: `create('', model)` and `update(id, model)` returning `ServiceCallResult` (inferred from project models).
- On success: navigate back to list using absolute path to avoid relative pitfalls.

Skeleton:
```ts
@Component({ selector: 'app-permission-form', imports: [/* UI + directives */], /* hostDirectives for Valid */ template: `...` })
export class PermissionForm {
	protected readonly _services = injectSecurityPermissions();
	protected readonly routeParams = toSignal(this._services.activatedRoute.params);
	protected readonly permissionId = computed(() => this.routeParams()?.['id'] as string);
	protected readonly model = computed(() => this._services.permissions.data().find(e => e.id === this.permissionId()) ?? new PermissionVM());
	protected async submit() { /* create or update; navigate on success */ }
	protected returnToList() { this._services.router.navigate(['/modules/security/permissions']); }
}
```

Rules:
- Always guard template usage with `@if (model()) { ... }` when referencing model fields.
- Disable identifier field when editing (`[disabled]="!!model().id"`).
- Provide delete panel only for existing entities.
- Keep navigation absolute for clarity.

---
### 9. Signals & Reactivity
Use `toSignal` to convert observables (route params, facade streams) into signals.
Use `computed` for derived values; avoid manual state tracking unless mutation needed.
Don’t store signals in global providers; they belong to components.

---
### 10. CRUD Operations Contract
Facade methods (inherited from `ApiFacade`): expected pattern:
- `data(): T` – current cached data.
- `create(parentId: string, dto: ModelVM)` – returns `{ success: boolean; ... }`.
- `update(id: string, dto: ModelVM)` – same shape.
- `saving()` signal/flag for in-progress state (see usage in submit button).
Always check `response.success` before navigation. On failure show error (extend pattern when needed – currently silent fail is tolerated but improvement recommended).

---
### 11. Validation & Directives
Use host directive pattern:
```ts
hostDirectives: [{ directive: Valid, outputs: ['isValidChange'] }]
```
Wrap form inputs in validation container `<ng-container validate>` and disable submit if the validation directive reports invalid.
Mark required fields with `[required]="true"` and rely on `Required` directive.

---
### 12. Navigation Rules
- After successful create/update/delete: navigate to the feature root list route.
- Use absolute path segments: `['/modules/security/permissions']` to avoid ambiguity.
- For row links in lists: return relative array (`['..', record.id]`) to support nested routes produced by `listRoute`.

---
### 13. Internationalization (i18n)
Current pattern: direct English strings + `translate` pipe (`{{ 'Permissions' | translate }}`). For new features:
1. Add keys to `public/i18n/en.json` & `pl.json` when introducing non-generic text (preferred future direction).
2. Use semantic keys: `permissions.title`, `permissions.fields.name`, not raw words.
3. Temporary rapid development may use raw strings with pipe; refactor to keys before merge.

---
### 14. UI & Styling
- Reuse shared UI primitives (`app-ui-panel`, buttons, table columns) – do not create ad-hoc markup if an existing component suffices.
- Keep component `styles: `` ` empty unless absolutely necessary; prefer global SCSS tokens in `styles/_variables.scss`.
- For layout, wrap action buttons inside the panel component using its start/end template regions.

---
### 15. Error Handling & Edge Cases
Handle missing entity on edit:
- If lookup returns `undefined`, either navigate back or create a placeholder model. Prefer navigation to avoid phantom entries.
Edge cases to consider:
- Empty list state (render “Create” button prominently).
- Slow network: show progress using `[isInProgress]="_services.<facade>.saving()"` binding.
- Duplicate creation attempts: disable submit while saving.
- Deleted entity while editing: if update fails with not found, navigate back.

---
### 16. Testing Guidelines (Minimal Baseline)
Add at least:
1. Facade test: sorting logic inside `withData()`.
2. Route config test: ensure `routes` exports array with one route containing both list and form loader functions.
3. Component shallow test: form component creates new `ModelVM` when id absent.
Testing style: prefer isolated tests; mock facade responses via subclass or spy.

---
### 17. Performance Considerations
- Lazy-load feature components (already done via dynamic imports).
- Keep list templates minimal; heavy computed logic should be inside `computed` signals.
- Avoid redundant sorting in component; keep sorting centralized in facade `withData()`.
- For image-heavy features consult `IMAGE_CACHING_SETUP.md`.
- For first-screen critical features check `LCP_OPTIMIZATION_SUMMARY.md` and avoid blocking calls in constructors.

---
### 18. Linting & Quality
- Run `npm run lint` before committing.
- No unused imports (see `UNUSED_IMPORTS_SETUP.md`).
- Keep constructor injection inside providers only; components use `inject()` for clarity.
- Avoid sprawling templates; if template exceeds ~120 lines, consider splitting into smaller presentational components.

---
### 19. Feature Creation Checklist
1. Create folder: `src/app/<features|modules/<module>>/<feature>`.
2. Add `entity.vm.ts` (View Model).
3. Add `entity.provider.ts` (Facade + provider helpers).
4. Add `entity-list.ts` and `entity-form.ts` components (standalone, with `imports` arrays).
5. Add `routes.ts` using `listRoute` helper.
6. Wire route in parent routing file (e.g., append lazy route to `modules/security/routes` if needed).
7. Add i18n keys to both `en.json` and `pl.json`.
8. Add minimal tests (`*.spec.ts`).
9. Verify CRUD flow manually (create -> edit -> delete -> list refresh).
10. Run lint & fix issues (`npm run lint:fix`).

---
### 20. Example Skeleton (Replace Permission with Your Entity)
```
my-entities/
	my-entity.vm.ts
	my-entity.provider.ts
	my-entity-list.ts
	my-entity-form.ts
	routes.ts
```

---
### 21. Extension / Future Improvements
For advanced features consider adding:
- `state.signal.ts` for complex derived aggregates.
- `entity.mapper.ts` for DTO <-> VM translation if divergence grows.
- Optimistic updates: update facade cache before server confirmation and rollback on failure.
- Centralized error toast service injection into provider object.

---
### 22. Consistency Rules (Do / Don’t)
Do:
- Reuse existing UI components.
- Keep files focused (one class per file).
- Use absolute navigation after mutations.
Don’t:
- Introduce NgModules for new features.
- Inject services directly into components when already exposed via provider wrapper.
- Perform business logic in templates.

---
### 23. Short Justification of Current Pattern
The provider + facade + VM triad keeps components thin, enables easy testing, and allows swapping backend endpoints with minimal surface change. Signals remove manual subscription cleanup and enable declarative derivations. Lazy routes keep bundle size controlled.

---
### 24. Quick Start (TL;DR)
1. Copy `permissions` folder → rename.
2. Replace names/endpoints.
3. Adjust VM fields.
4. Update i18n keys.
5. Add route lazy import in parent router.
6. Test CRUD + run lint.

Use this section for rapid iteration; refer to full sections for nuance.

---
End of guidelines.