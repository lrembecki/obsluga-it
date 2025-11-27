# Shared Forms System Documentation

## Overview

The shared forms system provides a declarative, schema-driven approach to building reactive forms in Angular. It abstracts the repetitive boilerplate of form creation, validation, and rendering into a reusable pattern that integrates seamlessly with the application's facade architecture.

**Location**: `src/app/shared/forms/`

**Philosophy**: Define form structure once via schema, render automatically, integrate with facade-based CRUD operations.

---

## Table of Contents

1. [Architecture Overview](#architecture-overview)
2. [Core Components](#core-components)
3. [Schema Definition](#schema-definition)
4. [Usage Patterns](#usage-patterns)
5. [Field Types & Controls](#field-types--controls)
6. [Validation](#validation)
7. [Dynamic Behavior](#dynamic-behavior)
8. [Integration with Facades](#integration-with-facades)
9. [Layout Options](#layout-options)
10. [Error Handling](#error-handling)
11. [Advanced Features](#advanced-features)
12. [Migration from Manual Forms](#migration-from-manual-forms)
13. [Best Practices](#best-practices)
14. [Troubleshooting](#troubleshooting)

---

## Architecture Overview

The forms system consists of several cooperating layers:

```
┌─────────────────────────────────────────┐
│         FormTemplate Component          │  ← High-level wrapper with submit/delete/return UI
├─────────────────────────────────────────┤
│         FormRenderer Component          │  ← Renders fields based on schema
├─────────────────────────────────────────┤
│      BaseFormComponent (Directive)      │  ← Abstract base for form logic
├─────────────────────────────────────────┤
│         FormSchema (Model)              │  ← Declarative field definitions
├─────────────────────────────────────────┤
│    FormFactoryService / FormRulesService│  ← Form creation & dynamic rules
├─────────────────────────────────────────┤
│     Individual Field Controls           │  ← TextInput, SelectInput, CheckboxInput, TextareaInput
└─────────────────────────────────────────┘
```

**Key Principles**:
- **Schema-driven**: Forms are defined declaratively via `FormSchema<T>`.
- **Type-safe**: Generic `T` ensures field keys match model properties.
- **Facade integration**: Direct integration with `ApiFacade` for CRUD operations.
- **Reactive**: Built on Angular Reactive Forms (`FormGroup`, `FormControl`).
- **Standalone**: All components are standalone; no NgModules.

---

## Core Components

### 1. FormSchema & Field Schema Classes

**File**: `form-schema.model.ts`

Defines the structure and behavior of forms.

#### FormSchema<T>

The root schema for a form.

```typescript
export class FormSchema<T> {
  fields: FormFieldSchema<T>[] = [];
  layout: FormSchemaLayout = 'two-column';
  layoutDisabled: boolean = false;
  canDelete: (data: any) => boolean = (_data) => false;

  constructor(init?: Partial<FormSchema<T>>) {
    Object.assign(this, init);
  }
}
```

**Properties**:
- `fields`: Array of field definitions.
- `layout`: `'single-column'` or `'two-column'`.
- `layoutDisabled`: If `true`, ignores layout class application.
- `canDelete`: Function determining if delete button should appear in edit mode.

#### FormFieldSchema<T>

Base class for all field types.

```typescript
export class FormFieldSchema<T> {
  key: keyof T = null!;
  label: string = null!;
  type: FormFieldType = null!;
  placeholder: string = null!;
  validators: any[] = [];
  disabled: boolean = false;
  disabledOnEdit?: boolean = false;
  disabledOnCreate?: boolean = false;
  dynamicDisabled?: (formValue: T) => boolean = (_v) => false;

  constructor(
    type: FormFieldType,
    init?: Partial<FormFieldSchema<T>>
  ) {
    Object.assign(this, init);
    this.type = type;
  }
}
```

**Properties**:
- `key`: Model property name (type-safe via `keyof T`).
- `label`: Display label.
- `type`: Field type (text, textarea, select, checkbox, number, date).
- `placeholder`: Placeholder text.
- `validators`: Array of Angular validators.
- `disabled`: Static disabled state.
- `disabledOnEdit`/`disabledOnCreate`: Conditional disabling based on mode.
- `dynamicDisabled`: Function to compute disabled state based on form value (reactive).

#### Specialized Field Schemas

- **TextFormFieldSchema<T>**: For text inputs.
- **TextareaFieldSchema<T>**: For multi-line text; adds `rows` and `cols` properties.
- **CheckboxFormFieldSchema<T>**: For boolean checkboxes.
- **SelectFormFieldSchema<T>**: For dropdowns; adds `options`, `clearable`, `itemTemplate`.

---

### 2. BaseFormComponent

**File**: `base-form.component.ts`

Abstract directive providing common form lifecycle logic.

```typescript
export abstract class BaseFormComponent<T> implements OnInit {
  form!: FormGroup;
  abstract schema: FormSchema<T>;
  mode: 'create' | 'edit' = 'create';
  formFactory = inject(FormFactoryService);

  ngOnInit() {
    this.form = this.formFactory.createForm(this.schema);
  }

  abstract submit(data: T): Promise<void>;

  async onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    await this.submit(this.form.value);
  }

  patch(data: Partial<T>) { /* ... */ }
  setEditMode() { this.mode = 'edit'; }
  setCreateMode() { this.mode = 'create'; }
}
```

**Responsibilities**:
- Form creation via `FormFactoryService`.
- Validation enforcement before submission.
- Mode tracking (create vs. edit).
- Patching data for edit scenarios.

**Usage**: Extend this directive when building custom form components (rare; typically use `FormTemplate` instead).

---

### 3. FormTemplate

**File**: `form-template.ts`

High-level component that combines schema, facade, and routing into a complete form experience.

**Injection Tokens**:
- `FormSchemaScope`: Provides the schema.
- `FormFacadeScope`: Provides the facade for CRUD operations.
- `FormReturnRouteScope`: Provides navigation path after success.

**Template**:
- Renders submit, delete (conditional), and return buttons.
- Embeds `FormRenderer` for field rendering.

**Features**:
- Automatically detects edit mode via route params (`:id`).
- Calls facade `create` or `update` methods on submit.
- Navigates to list on success.
- Integrates with `ButtonDelete` for entity removal.

**Example Usage**:

```typescript
// In route definition
{
  path: 'create',
  providers: [
    { provide: FormSchemaScope, useValue: mySchema },
    { provide: FormFacadeScope, useExisting: MyEntityFacade },
    { provide: FormReturnRouteScope, useValue: ['/entities'] },
  ],
  loadComponent: () => import('./form-template').then(m => m.FormTemplate),
}
```

---

### 4. FormRenderer

**File**: `form-renderer.ts`

Renders fields based on schema; delegates to specialized input components.

**Template Logic**:
```html
<form [formGroup]="form()" class="form-grid" [ngClass]="schema().layout">
  @for (field of schema().fields; track field) {
    <div class="form-field">
      @if (field.type === 'text') {
        <app-text-input [field]="field" [form]="form()" />
      }
      <!-- Other field types -->
      <app-error [control]="form().get(field.key)!"></app-error>
    </div>
  }
</form>
```

**Responsibilities**:
- Iterates over schema fields.
- Dynamically renders appropriate input component.
- Displays errors via `Error` component.
- Applies layout classes.

**Dynamic Rules**: Subscribes to form value changes and applies `FormRulesService` to enable/disable fields based on `dynamicDisabled` functions.

---

### 5. FormFactoryService

**File**: `services/form-factory.service.ts`

Creates `FormGroup` instances from schemas.

```typescript
createForm<T>(schema: FormSchema<T>, mode: 'create' | 'edit' = 'create'): FormGroup {
  const controls: any = {};

  schema.fields.forEach(field => {
    const isDisabled =
      field.disabled ||
      (field.disabledOnEdit && mode === 'edit') ||
      (field.disabledOnCreate && mode === 'create');

    controls[field.key] = new FormControl(
      { value: null, disabled: !!isDisabled },
      field.validators ?? []
    );
  });

  return new FormGroup(controls);
}
```

**Logic**:
- Computes initial disabled state based on mode and field configuration.
- Attaches validators.
- Returns ready-to-use `FormGroup`.

---

### 6. FormRulesService

**File**: `services/form-rule.service.ts`

Applies dynamic rules (conditional enable/disable) during form value changes.

```typescript
applyRules<T>(schema: FormSchema<T>, form: FormGroup, value: any) {
  schema.fields.forEach(field => {
    if (field.dynamicDisabled) {
      const shouldDisable = field.dynamicDisabled(value);
      const control = form.get(field.key as string);

      shouldDisable
        ? control?.disable({ emitEvent: false })
        : control?.enable({ emitEvent: false });
    }
  });
}
```

**Use Case**: Disable "End Date" field until "Start Date" is filled; enable "Discount Code" only when "Apply Discount" checkbox is checked.

---

## Schema Definition

### Basic Example

```typescript
interface User {
  userId: string;
  name: string;
  email: string;
  isActive: boolean;
}

const userSchema = new FormSchema<User>({
  fields: [
    new TextFormFieldSchema<User>({
      key: 'userId',
      label: 'User ID',
      placeholder: 'Enter unique identifier',
      disabledOnEdit: true,
      validators: [Validators.required],
    }),
    new TextFormFieldSchema<User>({
      key: 'name',
      label: 'Full Name',
      placeholder: 'John Doe',
      validators: [Validators.required, Validators.minLength(3)],
    }),
    new TextFormFieldSchema<User>({
      key: 'email',
      label: 'Email Address',
      placeholder: 'user@example.com',
      validators: [Validators.required, Validators.email],
    }),
    new CheckboxFormFieldSchema<User>({
      key: 'isActive',
      label: 'Active User',
    }),
  ],
  layout: 'two-column',
  canDelete: (data) => !!data.userId && !data.isActive,
});
```

**Notes**:
- `userId` disabled on edit (immutable identifier).
- `canDelete` allows deletion only for inactive users with an ID.

---

### Advanced Example with Dynamic Disable

```typescript
interface Subscription {
  id: string;
  plan: 'free' | 'premium';
  maxUsers: number;
}

const subscriptionSchema = new FormSchema<Subscription>({
  fields: [
    new SelectFormFieldSchema<Subscription>({
      key: 'plan',
      label: 'Plan Type',
      options: [
        { label: 'Free', value: 'free' },
        { label: 'Premium', value: 'premium' },
      ],
      validators: [Validators.required],
    }),
    new TextFormFieldSchema<Subscription>({
      key: 'maxUsers',
      label: 'Max Users',
      placeholder: '10',
      validators: [Validators.required, Validators.min(1)],
      dynamicDisabled: (formValue) => formValue.plan === 'free', // Disabled for free plan
    }),
  ],
});
```

**Behavior**: `maxUsers` field is disabled when `plan` is `'free'`.

---

## Usage Patterns

### Pattern 1: Using FormTemplate (Recommended)

**Route Configuration**:

```typescript
import { FormSchemaScope, FormFacadeScope, FormReturnRouteScope } from '@app/shared/forms/form-schema.model';

export const routes: Routes = [
  {
    path: 'users',
    providers: [provideUsers()],
    children: [
      {
        path: 'create',
        providers: [
          { provide: FormSchemaScope, useValue: userSchema },
          { provide: FormFacadeScope, useExisting: UserFacade },
          { provide: FormReturnRouteScope, useValue: ['/users'] },
        ],
        loadComponent: () => import('@app/shared/forms/form-template').then(m => m.FormTemplate),
      },
      {
        path: ':id',
        providers: [
          { provide: FormSchemaScope, useValue: userSchema },
          { provide: FormFacadeScope, useExisting: UserFacade },
          { provide: FormReturnRouteScope, useValue: ['/users'] },
        ],
        loadComponent: () => import('@app/shared/forms/form-template').then(m => m.FormTemplate),
      },
    ],
  },
];
```

**Benefits**:
- Zero custom form component code.
- Automatic create/edit detection.
- Integrated submit, delete, return buttons.
- Consistent UI/UX across all forms.

---

### Pattern 2: Extending BaseFormComponent (Custom Forms)

When you need custom UI or additional logic not covered by `FormTemplate`.

```typescript
@Component({
  selector: 'app-custom-user-form',
  imports: [FormRenderer, UiPanel, Button],
  template: `
    <h1>Custom User Form</h1>
    <app-ui-panel>
      <ng-template #start>
        <app-button text="Save" (buttonClick)="onSubmit()" [disabled]="form.invalid" />
      </ng-template>
    </app-ui-panel>
    <app-form-renderer [schema]="schema" [form]="form" />
  `,
})
export class CustomUserForm extends BaseFormComponent<User> {
  schema = userSchema;
  private facade = inject(UserFacade);

  async submit(data: User): Promise<void> {
    const response = await this.facade.create('', data);
    if (response.success) {
      // Custom navigation or notification
    }
  }
}
```

**Use Cases**:
- Multi-step wizards.
- Forms with embedded child components.
- Custom submit/cancel logic.

---

## Field Types & Controls

### Text Input

**Schema**:
```typescript
new TextFormFieldSchema<T>({
  key: 'fieldName',
  label: 'Field Label',
  placeholder: 'Enter text',
  validators: [Validators.required],
})
```

**Component**: `TextInput` (`controls/text-input.ts`)

**Template**:
```html
<label>{{ field().label }}</label>
<input pInputText [placeholder]="field().placeholder" [formControl]="form().get(field().key)">
```

**Styling**: Flex column layout with 0.25rem gap.

---

### Textarea Input

**Schema**:
```typescript
new TextareaFieldSchema<T>({
  key: 'description',
  label: 'Description',
  rows: 5,
  cols: 40,
  validators: [Validators.maxLength(500)],
})
```

**Component**: `TextareaInput` (`controls/textarea-input.ts`)

**Properties**:
- `rows`: Number of visible text rows (default: 3).
- `cols`: Width in characters (default: 30).

---

### Checkbox Input

**Schema**:
```typescript
new CheckboxFormFieldSchema<T>({
  key: 'isActive',
  label: 'Active',
})
```

**Component**: `CheckboxInput` (`controls/checkbox-input.ts`)

**Template**:
```html
<label>{{ field().label }}</label>
<p-checkbox [formControl]="form().get(field().key)" />
```

**Note**: Uses PrimeNG `p-checkbox`.

---

### Select Input (Dropdown)

**Schema**:
```typescript
new SelectFormFieldSchema<T>({
  key: 'status',
  label: 'Status',
  placeholder: 'Select status',
  options: [
    { label: 'Active', value: 'active' },
    { label: 'Inactive', value: 'inactive' },
  ],
  clearable: true,
  itemTemplate: CustomItemComponent, // Optional
  validators: [Validators.required],
})
```

**Component**: `SelectInput` (`controls/select-input.ts`)

**Properties**:
- `options`: Array of `{ label: string; value: any }`.
- `clearable`: Show clear button (default: `false`).
- `itemTemplate`: Optional custom component for rendering dropdown items.

**Template Features**:
- Uses PrimeNG `p-select`.
- Supports custom item templates via `NgComponentOutlet`.

---

## Validation

### Built-in Validators

Use Angular's built-in validators:

```typescript
import { Validators } from '@angular/forms';

new TextFormFieldSchema<T>({
  key: 'email',
  label: 'Email',
  validators: [
    Validators.required,
    Validators.email,
    Validators.minLength(5),
    Validators.maxLength(100),
  ],
})
```

### Custom Validators

**File**: `validators/email.validator.ts` (currently empty placeholder)

**Example Custom Validator**:

```typescript
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function emailValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const valid = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(control.value);
    return valid ? null : { invalidEmail: { value: control.value } };
  };
}
```

**Usage in Schema**:
```typescript
import { emailValidator } from '@app/shared/forms/validators/email.validator';

new TextFormFieldSchema<T>({
  key: 'email',
  validators: [Validators.required, emailValidator()],
})
```

### Error Display

**Component**: `Error` (`error.ts`)

```typescript
@Component({
  selector: 'app-error',
  imports: [Message],
  template: `
    @if (control() && control().touched && control().errors) {
      <p-message severity="error">
        {{ 'Pole zawiera błędy.' }}
      </p-message>
    }
  `,
})
export class Error {
  control = input<AbstractControl>(null!);
}
```

**Behavior**:
- Displays error message only when control is touched and has errors.
- Uses PrimeNG `p-message` component.

**Future Enhancement**: Map specific error keys to user-friendly messages (e.g., `required` → "This field is required").

---

## Dynamic Behavior

### Conditional Disabling

Use `dynamicDisabled` function to compute disabled state reactively.

**Example**:
```typescript
new TextFormFieldSchema<Subscription>({
  key: 'promoCode',
  label: 'Promo Code',
  dynamicDisabled: (formValue) => !formValue.applyPromo,
})
```

**Mechanism**:
- `FormRulesService` subscribes to form value changes.
- On each change, evaluates `dynamicDisabled` for all fields.
- Enables/disables controls without emitting events (prevents infinite loops).

---

### Mode-Based Disabling

**Static Disabling**:
- `disabled: true` → Always disabled.
- `disabledOnEdit: true` → Disabled only when editing existing entity.
- `disabledOnCreate: true` → Disabled only when creating new entity.

**Example**:
```typescript
new TextFormFieldSchema<User>({
  key: 'userId',
  label: 'User ID',
  disabledOnEdit: true, // Immutable after creation
})
```

---

## Integration with Facades

The forms system integrates deeply with the facade pattern (see `angular.instructions.md` sections 5-6).

### Facade Contract

Facades must extend `ApiFacade<T>` and implement:
- `data(): T` – Current cached data.
- `create(parentId: string, dto: Model)` – Returns `{ success: boolean }`.
- `update(id: string, dto: Model)` – Returns `{ success: boolean }`.
- `saving()` – Signal indicating save in progress.
- `getById(id: string): Model | undefined` (optional, used by `FormTemplate`).

### FormTemplate Integration

**Providers**:
```typescript
providers: [
  { provide: FormSchemaScope, useValue: mySchema },
  { provide: FormFacadeScope, useExisting: MyEntityFacade },
  { provide: FormReturnRouteScope, useValue: ['/path/to/list'] },
]
```

**Submit Flow** (from `FormTemplate.submit`):
```typescript
async submit(data: any): Promise<void> {
  const model = structuredClone(this.model()) ?? data;
  Object.assign(model, data);

  const response = this.id()
    ? await this.facade.update(this.id(), model)
    : await this.facade.create('', model);

  if (response.success) {
    this.returnToList();
  }
}
```

**Key Points**:
- Clones existing model (edit) or uses new data (create).
- Merges form value into model.
- Delegates to facade.
- Navigates on success.

---

## Layout Options

### Two-Column Layout (Default)

```typescript
layout: 'two-column'
```

**CSS** (applied via `ngClass` in `FormRenderer`):
```css
.form-grid.two-column {
  grid-template-columns: 1fr 1fr;
}
```

**Behavior**: Fields arrange in two columns on wider screens.

---

### Single-Column Layout

```typescript
layout: 'single-column'
```

**CSS**:
```css
.form-grid.single-column {
  grid-template-columns: 1fr;
}
```

**Use Case**: Forms with wide fields (textarea, long text) or mobile-first designs.

---

### Disabling Layout

```typescript
layoutDisabled: true
```

**Effect**: No layout class applied; falls back to default grid behavior.

---

## Error Handling

### Validation Errors

- Controlled at field level via `validators` array.
- Errors displayed via `Error` component beneath each field.
- Form submission blocked when `form.invalid` is `true`.

### Submit Errors

**Current Behavior** (from `FormTemplate`):
- Silent failure if `response.success` is `false`.

**Future Enhancement**:
- Display toast notification on failure.
- Provide error mapping from backend response.

**Workaround**: Override `submit` method in custom form components to handle errors explicitly.

---

## Advanced Features

### Custom Item Templates (Select)

**Use Case**: Display complex objects in dropdown options (e.g., user avatars, multi-line entries).

**Example**:

```typescript
@Component({
  selector: 'app-user-item',
  template: `<div>{{ item.label }} ({{ item.value }})</div>`,
})
export class UserItemComponent {
  item = input<{ label: string; value: string }>(null!);
}

new SelectFormFieldSchema<T>({
  key: 'assignedUser',
  options: userOptions,
  itemTemplate: UserItemComponent,
})
```

**Mechanism**: `SelectInput` uses `NgComponentOutlet` to render custom template.

---

### Lifecycle Hooks in Custom Forms

**`onAfterPatch` Hook** (from `BaseFormComponent`):

```typescript
patch(data: Partial<T>) {
  if (!this.form) return;
  this.form.patchValue(data);
  this.setEditMode();

  if ((this as any).onAfterPatch) {
    (this as any).onAfterPatch(data);
  }
}
```

**Usage**:
```typescript
export class MyForm extends BaseFormComponent<MyModel> {
  onAfterPatch(data: Partial<MyModel>) {
    // Custom logic after patching (e.g., fetch related data)
    console.log('Patched with:', data);
  }
}
```

---

## Migration from Manual Forms

### Before (Manual Form)

```typescript
@Component({
  template: `
    <form [formGroup]="formGroup">
      <label>Name</label>
      <input formControlName="name" />
      <label>Email</label>
      <input formControlName="email" />
      <button (click)="submit()">Submit</button>
    </form>
  `,
})
export class ManualForm {
  formGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  async submit() {
    if (this.formGroup.invalid) return;
    await this.facade.create('', this.formGroup.value);
  }
}
```

### After (Schema-Driven Form)

**Schema Definition**:
```typescript
const schema = new FormSchema<MyModel>({
  fields: [
    new TextFormFieldSchema({ key: 'name', label: 'Name', validators: [Validators.required] }),
    new TextFormFieldSchema({ key: 'email', label: 'Email', validators: [Validators.required, Validators.email] }),
  ],
});
```

**Route**:
```typescript
{
  path: 'create',
  providers: [
    { provide: FormSchemaScope, useValue: schema },
    { provide: FormFacadeScope, useExisting: MyFacade },
    { provide: FormReturnRouteScope, useValue: ['/list'] },
  ],
  loadComponent: () => import('@app/shared/forms/form-template').then(m => m.FormTemplate),
}
```

**Result**: No component code needed; form rendered and wired automatically.

---

## Best Practices

### 1. Keep Schemas Co-Located

Store schema definitions near the feature using them (e.g., in provider file or dedicated `schemas/` folder within feature).

### 2. Type-Safe Field Keys

Always use generic `<T>` to ensure `key` properties match model structure.

```typescript
// ✅ Good
new TextFormFieldSchema<User>({ key: 'email' })

// ❌ Bad (no type safety)
new TextFormFieldSchema({ key: 'emal' }) // Typo undetected
```

### 3. Reuse Schemas

If multiple routes share the same form (create/edit), define schema once and reference via `useValue`.

### 4. Validators in Schema, Not Template

Define all validation rules in schema; avoid mixing template-driven and reactive approaches.

### 5. Use `FormTemplate` by Default

Only extend `BaseFormComponent` when you need custom UI/logic that `FormTemplate` cannot accommodate.

### 6. Avoid Business Logic in Forms

Forms should remain thin:
- Validation → Validators.
- Transformation/persistence → Facade.
- Derived state → Signals.

### 7. Leverage Dynamic Disabling

Prefer `dynamicDisabled` over manual enable/disable calls in component lifecycle hooks.

### 8. Test Schemas

Verify schemas produce expected form structure via unit tests:

```typescript
it('should create form with correct controls', () => {
  const formGroup = formFactory.createForm(userSchema);
  expect(formGroup.get('email')).toBeTruthy();
  expect(formGroup.get('email')?.hasError('required')).toBe(true);
});
```

---

## Troubleshooting

### Issue: Field Not Rendering

**Symptoms**: Field defined in schema but not visible.

**Causes**:
1. `FormRenderer` missing import for field component.
2. Typo in field `type` (e.g., `'textt'` instead of `'text'`).
3. Conditional rendering logic excluding field.

**Solution**: Verify `FormRenderer` template includes `@if` block for field type; check `type` value.

---

### Issue: Validation Not Working

**Symptoms**: Form submits despite invalid fields.

**Causes**:
1. Validators not attached to schema field.
2. Submit button not checking `form.invalid`.

**Solution**:
- Ensure `validators` array is populated.
- Verify submit logic: `if (this.form.invalid) { this.form.markAllAsTouched(); return; }`.

---

### Issue: Dynamic Disable Not Triggering

**Symptoms**: `dynamicDisabled` function not evaluated on form changes.

**Causes**:
1. `FormRenderer.ngOnInit` typo (`ngOninit` instead of `ngOnInit`) prevents subscription.
2. Function logic error (returns wrong boolean).

**Solution**:
- Fix typo in `FormRenderer` (`ngOnInit`).
- Debug `dynamicDisabled` function with console logs.

---

### Issue: FormTemplate Not Detecting Edit Mode

**Symptoms**: Always treats form as create, even with `:id` param.

**Causes**:
1. Route param name mismatch (`id` vs. `userId`).
2. Facade `getById` method missing or returning `undefined`.

**Solution**:
- Ensure route uses `:id` param.
- Implement/fix `getById` in facade.

---

## Appendix: File Reference

| File | Purpose |
|------|---------|
| `form-schema.model.ts` | Schema class definitions & injection tokens |
| `base-form.component.ts` | Abstract base for form components |
| `form-template.ts` | High-level form wrapper with CRUD integration |
| `form-renderer.ts` | Renders fields based on schema |
| `error.ts` | Validation error display component |
| `controls/text-input.ts` | Text input control |
| `controls/textarea-input.ts` | Textarea control |
| `controls/checkbox-input.ts` | Checkbox control |
| `controls/select-input.ts` | Dropdown/select control |
| `services/form-factory.service.ts` | Creates `FormGroup` from schema |
| `services/form-rule.service.ts` | Applies dynamic disable rules |
| `validators/email.validator.ts` | Custom email validator (placeholder) |
| `validators/pesel.validator.ts` | Custom PESEL validator (placeholder) |

---

## Glossary

- **Schema**: Declarative definition of form structure (fields, validation, layout).
- **Facade**: Service layer wrapping API calls; provides CRUD operations.
- **Injection Token**: Angular DI token for providing schema/facade to `FormTemplate`.
- **Dynamic Disabling**: Conditional enable/disable based on form value.
- **Mode**: Create or edit state of the form.
- **Field Control**: Reusable component rendering a specific input type.

---

## Future Enhancements

1. **Error Message Mapping**: Map validation error keys to user-friendly messages via i18n.
2. **Async Validators**: Support asynchronous validation (e.g., username uniqueness check).
3. **Conditional Visibility**: Add `dynamicHidden` function to hide/show fields based on form state.
4. **Multi-Step Forms**: Extend schema to support wizard-style forms with steps.
5. **Custom Layouts**: Allow per-field layout hints (e.g., `span: 2` for full-width fields in two-column layout).
6. **Built-in Validators**: Populate `email.validator.ts` and `pesel.validator.ts` with implementations.
7. **Form State Persistence**: Automatically save draft state to localStorage.

---

## Related Documentation

- **Angular Guidelines**: `/.github/instructions/angular.instructions.md` (facade pattern, signals, standalone components)
- **Facade Interface**: `src/app/core/interfaces/facade.interface.ts`
- **Route Helper**: `src/app/core/helpers/route.helper.ts` (list/create/edit route generation)

---

**Document Version**: 1.0  
**Last Updated**: November 27, 2025  
**Maintained By**: Frontend Architecture Team
