---
applyTo: '**'
---
## Angular Project Feature Creation & Coding Guidelines

This document defines how to add NEW FEATURES (domain slices) to the project, based on the existing `permissions` example located at `src/app/modules/security/permissions`. Follow these rules whenever generating code, reviewing changes, or answering architecture questions. Keep changes consistent, minimal, and aligned with established patterns.

---
### 1. Core Principles
1. Standalone components only (no NgModules for new code) – use the `imports` array in `@Component`.
2. Keep features SMALL: co-locate components, providers, routes, and the view model in a single folder.
3. Signals-first: use Angular Signals for local and derived state. Prefer `computed`, `toSignal`, and minimal `effect` over manual subscriptions or imperative RxJS in components.
4. Use Facade pattern to wrap backend API calls; never call `ApiService` directly from components.
5. Keep UI logic declarative: templates should reflect state pulled from facades/providers.
6. Prefer immutability (create new objects instead of mutating arrays in-place) inside facades when transforming data.
7. Use Angular’s new template control flow: `@if`, `@else`, `@for`, and `@switch`. Do not introduce `*ngIf`, `*ngFor`, or `*ngSwitch` in new code. Migrate legacy usages opportunistically.

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
- Re-use facade methods: `create('', model)` and `update(id, model)` returning `ServiceCallResult`.
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
### 9. Signals & Reactivity (Mandatory)
- Use `toSignal(...)` to convert observables (route params, facade streams) into signals at the component boundary; avoid subscribing manually in components.
- Use `computed(...)` for all derived values; avoid redundant local state and imperative recomputation.
- Use `effect(...)` sparingly for side effects (e.g., logging or non-UI interactions); prefer handling navigation and API calls inside event handlers rather than effects when possible.
- Do not store signals in global providers; signals belong to components or small, focused local services when justified.
- Avoid `mutate`; use `set` and `update` for signal writes.
- Keep transformations pure; prefer structural sharing for arrays/objects.

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

Template control flow standard:
- Use `@if (...) { ... } @else { ... }` for conditional blocks.
- Use `@for (item of items; track item.id) { ... }` for lists; always provide a `track` expression.
- Use `@switch (expr) { @case ('a') { ... } @default { ... } }` for branching.
- Do not add new usages of `*ngIf`, `*ngFor`, or `*ngSwitch`. When touching templates, migrate any nearby legacy instances to the new control flow.

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
- **Follow UI Design System**: All UI decisions must align with `.github/instructions/ui-design.instructions.md` (design tokens, component patterns, accessibility, theming).
- Reuse shared UI primitives (`app-ui-panel`, buttons, table columns) – do not create ad-hoc markup if an existing component suffices.
- Keep component `styles: `` ` empty unless absolutely necessary; prefer global SCSS tokens in `styles/_variables.scss`.
- For layout, wrap action buttons inside the panel component using its start/end template regions.
- Templates MUST use the new control flow (`@if`, `@for`, `@switch`) for all conditional and loop constructs.
- When integrating Figma designs, use MCP tools (`mcp_figma_get_design_context`, `mcp_figma_create_design_system_rules`) and follow the design-to-code workflow in the UI Design System guide.

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
### 24. Dirty Editing Session with `cachedComputed`
Use `cachedComputed` (see `src/app/core/helpers/signal.helper.ts`) when a form edits an existing entity and needs a mutable working copy ("dirty session") separate from the pristine backend data until submit.

Pattern:
```ts
protected readonly model = cachedComputed(
	() => this._services.<facade>.data().find(e => e.id === this.entityId()) ?? new EntityVM(),
	entry => new EntityVM(entry)
);
```
Access fields via `model.session().field`. (Old pattern `model().field` is deprecated in forms now migrated.)

Call `model.update()` after structural mutations (adding/removing items, replacing arrays/objects). Pure primitive assignments via two‑way binding generally do not need `update()`.

Submit example:
```ts
protected async submit() {
	const session = this.model.session();
	const response = session.id
		? await this._services.entities.update(session.id, session)
		: await this._services.entities.create('', session);
	if (response.success) this.returnToList();
}
```

Template migration quick map:
| Old | New |
|-----|-----|
| `*ngIf="model()"` | `@if (model()) { ... }` |
| `*ngFor="let x of xs; trackBy: trackId"` | `@for (x of xs; track x.id) { ... }` |
| `*ngSwitchCase="val"` | `@switch (expr) { @case (val) { ... } }` |
| `@if (model())` | `@if (model.session())` |
| `[(value)]="model().name"` | `[(value)]="model.session().name"` |
| `model().id` | `model.session().id` |

---
### 25. Multi-Assignment Pattern (Assign Many Elements)
Standard approach (e.g. permission group assigning permissions):
```ts
protected readonly assignedPermissions = computed(() => allPermissions().filter(p => model.session().permissions.includes(p.id)));
protected readonly availablePermissions = computed(() => allPermissions().filter(p => !model.session().permissions.includes(p.id)));

addPermission(sel: DropdownInputComponent<string>) {
	if (!sel.value()) return;
	model.session().permissions = [...model.session().permissions, sel.value()!];
	sel.value.set(null!);
	model.update();
}

removePermission(record: PermissionVM) {
	model.session().permissions = model.session().permissions.filter(id => id !== record.id);
	model.update();
}
```
Guidelines:
1. Derive `assigned` / `available` via `computed` signals.
2. Use immutable array operations (`filter`, spread) before `model.update()`.
3. Hide add panel if `available.length === 0`.
4. Present a secondary heading for the collection (e.g. `<h2>Permissions</h2>`).

---
### 26. Migration Steps to `cachedComputed`
1. Import `cachedComputed`.
2. Replace `computed(() => ...)` with pattern above.
3. Update template bindings (`model()` → `model.session()`).
4. Adjust submit logic to use `session` object.
5. Add `model.update()` where structural changes occur.
6. Run lint + tests.

---
### 27. When NOT to Use `cachedComputed`
Prefer plain `computed` or direct facade signal when:
- Read-only presentations.
- Simple create forms (< 3 fields, no nested arrays) where session indirection adds no value.
- Proven performance-sensitive hotspots (measure before deviating).
In all cases above, continue to use Signals and new template control flow; do not revert to RxJS subscriptions or structural directives.

---
### 28. Future Enhancements (Optional)
- Add `reset()` to discard dirty edits.
- Implement diff extraction for audit logs.
- Optimistic updates with rollback using previous cache snapshot.

---
### 29. Cheat Sheet
| Concern | Pattern |
|---------|---------|
| Dirty edit state | `cachedComputed` + `session()` |
| Assign many items | `available/assigned` computed lists + immutable ops |
| Structural change | Replace array & call `model.update()` |
| Submit | Use `session` object |
| Revert (future) | `reset()` (planned) |

All existing forms in `security`, `trotamundos`, and `settings` now follow these patterns.

---
### 30. DTO ↔ View Model Mapping
Introduce DTO classes when the API payload shape diverges from the UI-centric View Model (VM) or when you need ID-only representations for collections (optimization & decoupling). Use the Permission Group feature as reference:

Files:
- `permission-group.vm.ts` – Rich UI model with nested `PermissionVM[]`.
- `permission-group.dto.ts` – Transport model holding primitive IDs (`permissions: string[]`).

Example VM:
```ts
export class PermissionGroupVM {
	id: string = null!;
	name: string = null!;
	permissions: PermissionVM[] = [];
	constructor(init?: Partial<PermissionGroupVM>) {
		Object.assign(this, init);
		this.permissions = init?.permissions?.map(p => new PermissionVM(p)) || [];
	}
}
```
Example DTO with mapping:
```ts
export class PermissionGroupDTO {
	id: string = null!;
	name: string = null!;
	permissions: string[] = [];
	constructor(init?: Partial<PermissionGroupDTO>) { Object.assign(this, init); }
	static fromVM(vm: PermissionGroupVM): PermissionGroupDTO {
		return new PermissionGroupDTO({
			id: vm.id,
			name: vm.name,
			permissions: vm.permissions
				.sort((a,b) => a.name.localeCompare(b.name))
				.map(p => p.id),
		});
	}
	static toVM(dto: PermissionGroupDTO, allPermissions: PermissionVM[]): PermissionGroupVM {
		return new PermissionGroupVM({
			id: dto.id,
			name: dto.name,
			permissions: dto.permissions
				.map(id => allPermissions.find(p => p.id === id)!)
				.filter(Boolean)
				.sort((a,b) => a.name.localeCompare(b.name))
		});
	}
}
```
Mapping Guidelines:
1. Keep transformations pure—never mutate input DTO or VM; return new instances.
2. Sort collections in one place (DTO→VM or VM→DTO) to avoid duplicate sorting downstream.
3. Resolve IDs to full objects using a facade-provided cache (`allPermissions` above). Handle missing references gracefully (filter out `undefined`).
4. Do not place heavy business logic inside DTO/VM; keep them as simple data carriers plus minimal mapping helpers.
5. Use `fromVM` when preparing payload for create/update; use `toVM` when hydrating data fetched from API.

---
### 31. Working With DTOs in Forms
1. Fetch raw DTO array via facade → map to VMs only if UI needs nested objects.
2. For large lists, keep session editing on a DTO-like structure (IDs only) using `cachedComputed`, then hydrate back to VM after persistence for display lists.
3. Submit path: `const dto = PermissionGroupDTO.fromVM(model.session()); await facade.update(dto.id, dto);` (or `create('', dto)` if no id).
4. Avoid re-sorting in both directions; choose one canonical sort point (above uses VM→DTO for stable order).

---
### 32. Migration Checklist (Adding DTO Layer)
1. Identify divergence (e.g. UI needs nested objects, API uses IDs).
2. Create `<entity>.dto.ts` with fields mirroring API.
3. Add static `fromVM` and `toVM` helpers.
4. Update facade: store DTOs or VMs? Prefer storing VMs if most components need rich objects; otherwise keep DTOs and expose derived VMs via computed.
5. Update form: choose editing target (DTO or VM). If editing many IDs, prefer DTO session.
6. Adjust tests: ensure mapping functions produce expected shapes & sorted order.

---
### 33. Quick Reference Table
| Concern | Pattern |
|---------|--------|
| Dirty edit state | `cachedComputed` + `session()` |
| Assign many items | `available/assigned` computed lists + immutable ops |
| DTO creation | `DTO.fromVM(vm)` |
| VM hydration | `DTO.toVM(dto, cache)` |
| Structural change | Replace array & call `model.update()` |
| Submit | Build DTO from session then call facade |
| Conditional rendering | `@if(...) { ... } @else { ... }` |
| List rendering | `@for (x of xs; track x.id) { ... }` |
| Branching | `@switch (expr) { @case (...) { ... } @default { ... } }` |
| Derived state | `computed(...)` |
| Observable → Signal | `toSignal(obs)` |

---
### 34. When NOT to Introduce DTO Layer
Skip DTO if API shape already matches VM and there is no performance or separation benefit. Revisit when:
- Collections grow large and nested objects cause payload bloat.
- Multiple components need different subsets of fields (opt for DTO + selective VM hydration).

End of guidelines.