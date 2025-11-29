---
applyTo: '**'
---
# UI Design System & Component Guidelines

This document defines the design system, UI component usage patterns, styling conventions, and visual design principles for this Angular application. Follow these rules when implementing UI features, creating new components, or integrating designs from Figma.

---
## 1. Design System Foundation

### 1.1 Architecture Overview
This project uses a **hybrid design system** combining:
- **PrimeNG** (v20.0.0) as the foundational UI library
- **Custom wrapper components** in `src/app/shared/ui/` for project-specific patterns
- **SCSS tokens** defined in `src/styles/_variables.scss` for design consistency
- **PrimeUIX themes** for theming capabilities

### 1.2 Design Tokens Location
All design tokens are centralized in:
```
src/styles/_variables.scss
```

These tokens define:
- Color palette (primary, secondary, success, danger, warning, info, light, dark)
- Typography (font family, sizes, line heights)
- Spacing scale (xs, sm, md, lg, xl)
- Responsive breakpoints
- Border radius variants
- Box shadow levels

---
## 2. Color System

### 2.1 Primary Palette
```scss
$primary-color: #007bff;    // Brand primary - use for CTAs, links, primary actions
$secondary-color: #6c757d;  // Neutral actions, secondary buttons
$success-color: #28a745;    // Success states, confirmations
$danger-color: #dc3545;     // Errors, destructive actions, warnings
$warning-color: #ffc107;    // Caution states
$info-color: #17a2b8;       // Informational messages
$light-color: #f8f9fa;      // Light backgrounds, subtle borders
$dark-color: #343a40;       // Dark text, headers
```

### 2.2 Theme Support
The application supports **light and dark themes**. Custom components use CSS custom properties for theme-aware colors:

**Light theme variables:**
```css
--txt: #2e2e2e;
--muted: #6b7280;
--border: #e6e6ea;
--ring: #4dabf7;
--hover: #eef5ff;
--bg: #ffffff;
--bg-head: #f8f9fa;
--bg-alt: #f8f9fa;
```

**Dark theme variables:**
```css
--txt: #e6e6e6;
--muted: #9ca3af;
--border: #3f3f56;
--ring: #60a5fa;
--hover: #2d3748;
--bg: #1e1e2f;
--bg-head: #252538;
--bg-alt: #252538;
```

### 2.3 Color Usage Rules
1. **Never hardcode colors** in component styles; use SCSS variables or CSS custom properties.
2. **Use semantic color names** (`$primary-color`) not literal color values (`#007bff`).
3. For theme-aware components, define CSS custom properties scoped to `.app-light` and `.app-dark` classes.
4. PrimeNG component colors use the `severity` prop: `'primary' | 'secondary' | 'success' | 'info' | 'warning' | 'danger'`.

---
## 3. Typography

### 3.1 Font Family
Primary typeface: **Atkinson Hyperlegible Next** (optimized for accessibility and readability)
```scss
$font-family-base: "Atkinson Hyperlegible Next", sans-serif;
```

### 3.2 Font Loading
Fonts are loaded via Google Fonts CDN in `src/styles.scss`:
```scss
@import url("https://fonts.googleapis.com/icon?family=Material+Icons");
```

Icon fonts: **Material Icons** and **PrimeIcons** are both available.

### 3.3 Type Scale
```scss
$font-size-base: 1rem;       // 16px base
$line-height-base: 1.5;      // Optimal readability
```

Use relative units (`rem`, `em`) for font sizes to support user preference scaling.

### 3.4 Typography Best Practices
- Avoid font sizes below `0.75rem` (12px) for body text (accessibility).
- Maintain minimum contrast ratio of 4.5:1 for text (WCAG AA standard).
- Use `font-weight: 600` for headings, `400` for body text.
- Preserve letter spacing for small caps or uppercase labels: `letter-spacing: 0.2px`.

---
## 4. Spacing System

### 4.1 Spacing Scale
```scss
$spacing-unit: 1rem;     // Base unit (16px)
$spacing-xs: 0.25rem;    // 4px
$spacing-sm: 0.5rem;     // 8px
$spacing-md: 1rem;       // 16px
$spacing-lg: 1.5rem;     // 24px
$spacing-xl: 3rem;       // 48px
```

### 4.2 Utility Classes
The project includes generated spacing utilities in `src/styles.scss`:
```scss
// Auto-generated classes: .mb-1 through .mb-5, .mt-1 through .mt-5, .p-1 through .p-5
.mb-3 { margin-bottom: 1rem; }
.mt-2 { margin-top: 0.5rem; }
.p-1 { padding: 1rem; }
```

### 4.3 Spacing Conventions
- **Component internal gaps**: Use `gap: 1rem` (or `$spacing-md`) in flex/grid layouts.
- **Form field spacing**: `gap: 2rem` between form groups.
- **Panel margins**: `margin-bottom: 1rem` for consistent vertical rhythm.
- **Avoid magic numbers**: Always use spacing tokens, not arbitrary pixel values.

---
## 5. Component Library Structure

### 5.1 Component Organization
```
src/app/shared/ui/
├── button/              # Custom button wrapper (PrimeNG pButton)
├── inputs/              # Form input components (text, dropdown, date, etc.)
├── table/               # Custom table implementation
├── ui-panel.ts          # Flexible layout panel
├── ui-table.ts          # Data table with sorting/filtering
├── ui-dialog.ts         # Modal dialog wrapper
├── icon/                # Icon component
├── navbar/              # Navigation bar
├── footer/              # Footer component
├── breadcrumbs/         # Breadcrumb navigation
├── loading/             # Loading spinner
└── upload/              # File upload component
```

### 5.2 Shared UI Primitives
Reuse these components consistently across features:

| Component | Selector | Purpose |
|-----------|----------|---------|
| `Button` | `<app-button>` | All clickable actions (submit, cancel, navigate) |
| `UiPanel` | `<app-ui-panel>` | Horizontal layout container with start/end slots |
| `UiTable` | `<app-ui-table>` | Data tables with columns, sorting, filtering |
| `TextInputComponent` | `<app-text-input>` | Text/email/password input fields |
| `DropdownInputComponent` | `<app-dropdown-input>` | Single-select dropdown |
| `DateInputComponent` | `<app-date-input>` | Date picker |
| `CheckboxInputComponent` | `<app-checkbox-input>` | Checkbox input |
| `UiDialog` | `<app-ui-dialog>` | Modal dialogs |

---
## 6. Button Component Pattern

### 6.1 Usage
```typescript
import { Button } from 'app/shared/ui/button/button';

@Component({
  imports: [Button],
  template: `
    <app-button 
      [text]="'Save'" 
      [color]="'primary'" 
      [icon]="'pi pi-check'" 
      [isInProgress]="saving()" 
      (buttonClick)="handleSave($event)"
    />
  `
})
```

### 6.2 Button Properties
| Input | Type | Description |
|-------|------|-------------|
| `text` | `string` | Button label (passed through i18n `translate` pipe) |
| `icon` | `string` | Icon class (Material Icons or PrimeIcons) |
| `color` | `ButtonSeverity` | `'primary' \| 'secondary' \| 'success' \| 'danger' \| 'warning' \| 'info'` |
| `disabled` | `boolean` | Disable button interaction |
| `isInProgress` | `boolean` | Show spinner, disable button |

### 6.3 Button Color Mapping
- **Primary actions** (save, submit, create): `color="primary"`
- **Secondary actions** (cancel, back): `color="secondary"`
- **Destructive actions** (delete, remove): `color="danger"`
- **Confirmations**: `color="success"`

### 6.4 Button States
- **Default**: Enabled, idle
- **Hover**: Visual feedback (handled by PrimeNG)
- **Disabled**: `[disabled]="true"` grays out, prevents clicks
- **In Progress**: `[isInProgress]="true"` shows spinner, disables interaction

---
## 7. Form Input Components

### 7.1 Standard Input Pattern
All input components follow this interface:
```typescript
@Component({
  hostDirectives: [
    HostControlDirective,
    { directive: Valid, inputs: ['valid'] }
  ],
  host: { class: 'input-container' }
})
export class TextInputComponent {
  label = input<string>();
  disabled = input<boolean>(false);
  value = model<string | undefined>();
  valueChange = output<string | undefined>();
}
```

### 7.2 Input Component Usage
```html
<app-text-input 
  [label]="'Username'" 
  [(value)]="model.session().username" 
  [disabled]="false"
/>

<app-dropdown-input 
  [label]="'Role'" 
  [(value)]="model.session().roleId" 
  [options]="roles()" 
  [optionLabel]="'name'" 
  [optionValue]="'id'"
/>

<app-date-input 
  [label]="'Birth Date'" 
  [(value)]="model.session().birthDate"
/>
```

### 7.3 Input Validation
- Use `Valid` host directive for real-time validation feedback.
- Wrap inputs in `<ng-container validate>` for grouped validation.
- Mark required fields with `[required]="true"`.
- Validation state changes emit via `isValidChange` output.

### 7.4 Input Accessibility
All input components:
- Generate unique IDs (`crypto.randomUUID()`) for label association.
- Include `<label [for]="id">` for screen reader support.
- Support keyboard navigation (built into PrimeNG base components).
- Respect `disabled` state for focus management.

---
## 8. Layout Components

### 8.1 UiPanel Component
**Purpose**: Horizontal layout container with flexible start/end content zones.

```html
<app-ui-panel>
  <ng-template #start>
    <app-button text="Create" color="primary" routerLink="create" />
  </ng-template>
  
  <ng-template #end>
    <app-button text="Export" icon="pi pi-download" />
  </ng-template>
</app-ui-panel>
```

**Layout behavior:**
- If only `#start` provided: content left-aligned, fills right space.
- If only `#end` provided: content right-aligned, fills left space.
- If both: start left, end right, space-between.
- If neither: default slot centered.

### 8.2 Panel Styling
```scss
:host {
  display: flex;
  justify-content: space-between;
  margin-bottom: 1rem;
}

.container {
  display: flex;
  gap: 1rem; // Consistent spacing between items
}
```

---
## 9. Table Component

### 9.1 Table Structure
```html
<app-ui-table [data]="items()" [loading]="facade.loading()">
  <app-ui-table-column 
    text="Name" 
    field="name" 
    [link]="true" 
    [renderLink]="renderLink" 
  />
  
  <app-ui-table-column 
    text="Email" 
    field="email" 
  />
  
  <app-ui-table-column 
    text="Created" 
    field="createdAt" 
    [width]="'120px'"
  />
</app-ui-table>
```

### 9.2 Table Column Types
| Component | Purpose |
|-----------|---------|
| `UiTableColumn` | Standard text column |
| `UiTableColumnLink` | Clickable router link column |
| `UiTableColumnDate` | Formatted date column |
| `UiTableColumnDropdown` | Inline dropdown editor |
| `UiTableColumnTextInput` | Inline text editor |
| `UiTableColumnCellTemplate` | Custom template column |

### 9.3 Table Theming
Tables automatically adapt to light/dark theme via CSS custom properties (see Section 2.2).

### 9.4 Table Responsive Behavior
- **Desktop**: Full table with sticky header.
- **Mobile** (<720px): Horizontal scroll with minimum width (600px).
- **Max height**: `calc(100dvh - 16rem)` prevents excessive vertical scroll.

### 9.5 Table Best Practices
- Always provide `[data]` as a signal: `[data]="items()"`.
- Use `[loading]="facade.loading()"` to show visual feedback during API calls.
- Specify `track` in `@for` loops for performance (already handled internally).
- Set column widths for date/action columns: `[width]="'120px'"`.
- Use `[link]` for clickable columns navigating to detail views.

---
## 10. Icon System

### 10.1 Available Icon Sets
1. **Material Icons** (Google Fonts): `<i class="material-icons">add_circle</i>`
2. **PrimeIcons**: `<i class="pi pi-check"></i>`

### 10.2 Icon Usage in Buttons
```html
<app-button 
  text="Save" 
  icon="pi pi-check" 
  color="primary" 
/>
```

### 10.3 Icon Definitions
Type-safe icon constants in `src/app/core/defaults/icon.default.ts`:
```typescript
export const Icons = ['add_circle'] as const;
export type IconType = (typeof Icons)[number];
```

Add new icons to this file for autocomplete support.

### 10.4 Icon Best Practices
- Prefer PrimeIcons (`pi pi-*`) for UI consistency with PrimeNG.
- Use Material Icons for domain-specific or unique icons.
- Always include `aria-label` for icon-only buttons (accessibility).
- Icon size in buttons: `16px` (handled by PrimeNG button styling).

---
## 11. Modal Dialogs

### 11.1 Dialog Component Usage
```typescript
import { UiDialog } from 'app/shared/ui/ui-dialog';

@Component({
  imports: [UiDialog],
  template: `
    <app-ui-dialog 
      [visible]="showDialog()" 
      [header]="'Confirm Action'" 
      (visibleChange)="showDialog.set($event)"
    >
      <p>Are you sure?</p>
      
      <ng-template #footer>
        <app-button text="Cancel" (buttonClick)="showDialog.set(false)" />
        <app-button text="Confirm" color="primary" (buttonClick)="confirm()" />
      </ng-template>
    </app-ui-dialog>
  `
})
```

### 11.2 Dialog Patterns
- Use signals for `visible` state: `showDialog = signal(false)`.
- Provide `#footer` template for action buttons.
- Keep dialogs **small and focused** (single task).
- Include cancel/close action for all dialogs (accessibility).

---
## 12. Styling Conventions

### 12.1 Component Styles
- **Prefer `:host` selector** for component root styles:
  ```scss
  :host {
    display: flex;
    flex-direction: column;
    gap: 1rem;
  }
  ```
- **Use `styles: ``** for inline styles (keep minimal).
- **Avoid external `.scss` files** unless component complexity justifies separation.

### 12.2 SCSS Usage
Import variables using modern `@use` syntax:
```scss
@use 'src/styles/variables' as vars;

.container {
  padding: vars.$spacing-md;
  color: vars.$primary-color;
}
```

### 12.3 Global Styles
Location: `src/styles.scss`

**Rules:**
- Only add truly global utilities (spacing, typography resets).
- Form-specific styles (flex layout, gap) are global for consistency.
- Avoid component-specific styles in global file.

### 12.4 CSS Custom Properties
For theme-aware components, define scoped custom properties:
```scss
::ng-deep .app-light .my-component {
  --bg: #ffffff;
  --txt: #2e2e2e;
}

::ng-deep .app-dark .my-component {
  --bg: #1e1e2f;
  --txt: #e6e6e6;
}

.my-component {
  background: var(--bg);
  color: var(--txt);
}
```

### 12.5 Avoid `::ng-deep` Except for Theming
- Use `::ng-deep` **only** for global theme variable scoping (`.app-light`, `.app-dark`).
- Do not use `::ng-deep` to override PrimeNG component internals (breaks encapsulation).
- Prefer wrapper components or CSS custom properties for customization.

---
## 13. Responsive Design

### 13.1 Breakpoints
```scss
$breakpoint-xs: 576px;
$breakpoint-sm: 768px;
$breakpoint-md: 992px;
$breakpoint-lg: 1200px;
$breakpoint-xl: 1400px;
```

### 13.2 Mobile-First Approach
Write styles for mobile first, then enhance for larger screens:
```scss
.container {
  flex-direction: column; // Mobile default
  
  @media (min-width: $breakpoint-md) {
    flex-direction: row; // Desktop
  }
}
```

### 13.3 Responsive Utilities
- Use `dvh` units for viewport-relative heights: `height: 100dvh`.
- Use `flex-wrap` for adaptive layouts.
- Avoid fixed widths; prefer `max-width` with percentage fallbacks.

### 13.4 Touch Targets
- Minimum button size: `44px × 44px` (WCAG AAA guideline).
- Increase padding on mobile for easier tapping.

---
## 14. Accessibility (a11y)

### 14.1 Semantic HTML
- Use `<button>` for actions, `<a>` for navigation.
- Provide `<label>` for all form inputs.
- Use `<table>`, `<thead>`, `<tbody>` for data tables (not divs).

### 14.2 ARIA Attributes
- Add `role="grid"` to custom table components.
- Use `aria-label` for icon-only buttons.
- Set `aria-disabled="true"` when disabling interactive elements.

### 14.3 Keyboard Navigation
- All interactive elements must be keyboard-accessible (`tabindex` management).
- Support `Enter` and `Space` for button activation (PrimeNG handles this).
- Trap focus inside modal dialogs (PrimeNG Dialog handles this).

### 14.4 Color Contrast
- Maintain WCAG AA contrast ratios (4.5:1 for text, 3:1 for UI components).
- Test with browser DevTools color contrast analyzer.
- Avoid relying solely on color to convey information (use icons/text).

### 14.5 Screen Reader Support
- All images need `alt` text.
- Loading states should announce changes (use `aria-live` regions).
- Form validation errors should be associated with inputs via `aria-describedby`.

---
## 15. Performance Optimization

### 15.1 Lazy Loading
- Feature components are lazy-loaded via dynamic imports in routes:
  ```typescript
  () => import('./entity-list').then(m => m.EntityList)
  ```
- Heavy UI components (charts, editors) should be lazy-loaded.

### 15.2 Signal-Based Reactivity
- Use `computed()` for derived values to avoid redundant calculations.
- Minimize `effect()` usage; prefer event handlers.
- Use `toSignal()` to convert observables at component boundaries.

### 15.3 Change Detection
- All shared UI components use `OnPush` implicitly via signals.
- Avoid mutating arrays/objects in-place; create new references.

### 15.4 Bundle Size
- Keep component imports minimal (import only what's used).
- Use PrimeNG tree-shakeable modules (not monolithic import).
- Run `source-map-explorer` to analyze bundle composition.

---
## 16. Design-to-Code Workflow (Figma Integration)

### 16.1 Using Figma MCP Tools
When implementing designs from Figma:

1. **Extract design context:**
   ```
   Use mcp_figma_get_design_context with nodeId and fileKey
   ```

2. **Review generated code** for:
   - Component structure alignment with existing patterns
   - Color token usage (replace hardcoded values with SCSS variables)
   - Spacing consistency (replace magic numbers with spacing tokens)

3. **Map Figma components to existing UI library:**
   | Figma Component | App Component |
   |-----------------|---------------|
   | Button | `<app-button>` |
   | Input Field | `<app-text-input>` |
   | Dropdown | `<app-dropdown-input>` |
   | Table | `<app-ui-table>` |
   | Modal | `<app-ui-dialog>` |

4. **Adapt generated styles:**
   - Replace pixel values with SCSS variables.
   - Use CSS custom properties for theme-aware colors.
   - Apply responsive breakpoints.

### 16.2 Design System Rules Generation
Run `mcp_figma_create_design_system_rules` to generate mapping rules for:
- Token alignment (Figma variables → SCSS variables)
- Component naming conventions
- Asset export settings

### 16.3 Asset Management
- **Images**: Store in `public/` directory.
- **Icons**: Add to PrimeIcons or Material Icons (avoid custom SVGs unless necessary).
- **Logos**: Use cached image component (`<app-cached-image>`).
- **Optimization**: Use WebP format, provide fallbacks.

---
## 17. Common UI Patterns

### 17.1 List/Detail Pattern
```html
<!-- List view -->
<app-ui-panel>
  <ng-template #start>
    <app-button text="Create" color="primary" routerLink="../create" />
  </ng-template>
</app-ui-panel>

<app-ui-table [data]="items()">
  <app-ui-table-column text="Name" field="name" link [renderLink]="renderLink" />
</app-ui-table>

<!-- Detail view (form) -->
<app-ui-panel>
  <ng-template #start>
    <app-button text="Back" routerLink=".." />
  </ng-template>
  <ng-template #end>
    @if (model.session().id) {
      <app-button text="Delete" color="danger" (buttonClick)="delete()" />
    }
  </ng-template>
</app-ui-panel>

<form (ngSubmit)="submit()">
  <app-text-input label="Name" [(value)]="model.session().name" />
  <app-button text="Save" color="primary" type="submit" />
</form>
```

### 17.2 Empty State Pattern
```html
@if (items().length === 0) {
  <div class="empty-state">
    <i class="pi pi-inbox" style="font-size: 3rem; color: var(--muted);"></i>
    <p>No items found.</p>
    <app-button text="Create First Item" color="primary" routerLink="create" />
  </div>
}
```

### 17.3 Loading State Pattern
```html
<app-ui-table [data]="items()" [loading]="facade.loading()">
  <!-- columns -->
</app-ui-table>

<!-- For buttons -->
<app-button 
  text="Save" 
  [isInProgress]="facade.saving()" 
  (buttonClick)="submit()"
/>
```

### 17.4 Error State Pattern
```html
@if (error()) {
  <div class="error-message">
    <i class="pi pi-exclamation-triangle"></i>
    <span>{{ error() }}</span>
  </div>
}
```

---
## 18. Component Creation Checklist

When creating a new UI component:

1. ✅ **Location**: Place in `src/app/shared/ui/` if reusable, or feature-specific folder if domain-coupled.
2. ✅ **Standalone**: Always use `standalone: true` (or omit, defaults to true in modern Angular).
3. ✅ **Imports**: Only import what's needed (tree-shaking).
4. ✅ **Selector**: Use `app-` prefix for custom components.
5. ✅ **Signals**: Use `input()`, `model()`, `output()`, `computed()` for reactivity.
6. ✅ **Styles**: Inline with `:host` selector; keep minimal.
7. ✅ **Accessibility**: Include labels, ARIA attributes, keyboard support.
8. ✅ **Theming**: Use CSS custom properties for colors if theme-aware.
9. ✅ **Documentation**: Add JSDoc comments for public API.
10. ✅ **Testing**: Add unit tests for logic, accessibility checks.

---
## 19. Anti-Patterns to Avoid

### 19.1 Styling Anti-Patterns
❌ **Don't hardcode colors:**
```scss
// Bad
color: #007bff;

// Good
color: vars.$primary-color;
```

❌ **Don't use magic numbers:**
```scss
// Bad
margin-bottom: 17px;

// Good
margin-bottom: vars.$spacing-md;
```

❌ **Don't override PrimeNG internals directly:**
```scss
// Bad
::ng-deep .p-button { padding: 10px; }

// Good - use wrapper component or CSS custom properties
```

### 19.2 Component Anti-Patterns
❌ **Don't create duplicate components** when existing UI primitives suffice.

❌ **Don't mix presentation and logic** in single component (prefer facade pattern).

❌ **Don't use structural directives** (`*ngIf`, `*ngFor`) in new code; use control flow (`@if`, `@for`).

---
## 20. Design Review Criteria

Before merging UI changes, verify:

1. ✅ **Visual consistency**: Matches existing component styles.
2. ✅ **Token usage**: No hardcoded colors, spacing, or font sizes.
3. ✅ **Responsive**: Works on mobile (320px) through desktop (1920px).
4. ✅ **Accessibility**: Passes axe DevTools scan (zero critical issues).
5. ✅ **Theming**: Adapts correctly to light/dark mode.
6. ✅ **Performance**: No unnecessary re-renders (check with Angular DevTools).
7. ✅ **Browser support**: Tested in Chrome, Firefox, Safari, Edge.

---
## 21. Migration from Figma Design

### 21.1 Step-by-Step Process
When implementing a Figma design:

1. **Extract node ID** from Figma URL:
   ```
   https://figma.com/design/:fileKey/:fileName?node-id=123-456
   → nodeId: "123:456"
   → fileKey: ":fileKey"
   ```

2. **Call MCP tool** to get design context:
   ```typescript
   mcp_figma_get_design_context(nodeId, fileKey)
   ```

3. **Review generated code**:
   - Check component structure
   - Identify reusable patterns
   - Note custom styling

4. **Map to existing components**:
   - Replace generic buttons with `<app-button>`
   - Replace inputs with `<app-text-input>`, etc.
   - Reuse table component instead of custom markup

5. **Adapt styles**:
   - Replace hex colors with SCSS variables
   - Replace pixel spacing with spacing tokens
   - Add responsive breakpoints

6. **Implement in feature**:
   - Create view model if needed
   - Wire up facade for data
   - Add validation directives
   - Connect routing

7. **Test**:
   - Visual regression (compare with Figma)
   - Responsive behavior
   - Accessibility audit
   - Theme switching

### 21.2 Figma-to-Code Mapping Reference
| Figma Element | Code Implementation |
|---------------|---------------------|
| Frame with Auto Layout | `display: flex; gap: 1rem;` |
| Variant Component | `@switch` control flow or `[ngClass]` |
| Boolean Property | `@if` control flow |
| Text Styles | SCSS typography variables |
| Color Styles | SCSS color variables + CSS custom properties |
| Effects (Shadow) | SCSS shadow variables |
| Spacing (Padding) | SCSS spacing variables |

---
## 22. Future Enhancements

### 22.1 Planned Design System Improvements
- **Design tokens in JSON format** for multi-platform support (mobile, email).
- **Storybook integration** for component documentation and testing.
- **Automated visual regression testing** (Chromatic or Percy).
- **Component usage analytics** to identify unused components.

### 22.2 Component Roadmap
- Advanced data grid with inline editing
- Rich text editor wrapper
- File upload with drag-and-drop
- Toast notification service
- Skeleton loading states

---
## 23. Resources & Tools

### 23.1 Design Tools
- **Figma**: Primary design source
- **Figma MCP Tools**: `mcp_figma_get_design_context`, `mcp_figma_create_design_system_rules`

### 23.2 Development Tools
- **PrimeNG Docs**: https://primeng.org/
- **Material Icons**: https://fonts.google.com/icons
- **PrimeIcons**: https://primeng.org/icons
- **axe DevTools**: Accessibility testing (browser extension)
- **Angular DevTools**: Performance profiling

### 23.3 Reference Documentation
- Angular Guidelines: `.github/instructions/angular.instructions.md`
- SCSS Setup: `SCSS_SETUP_GUIDE.md`
- Image Caching: `IMAGE_CACHING_SETUP.md`
- LCP Optimization: `LCP_OPTIMIZATION_SUMMARY.md`

---
## 24. Quick Reference

### 24.1 Component Import Cheatsheet
```typescript
// Buttons
import { Button } from 'app/shared/ui/button/button';

// Inputs
import { TextInputComponent } from 'app/shared/ui/inputs/text-input.component';
import { DropdownInputComponent } from 'app/shared/ui/inputs/dropdown-input.component';
import { DateInputComponent } from 'app/shared/ui/inputs/date-input.component';

// Layout
import { UiPanel } from 'app/shared/ui/ui-panel';
import { UiTable, UiTableColumn } from 'app/shared/ui/ui-table';

// Dialogs
import { UiDialog } from 'app/shared/ui/ui-dialog';

// Directives
import { Valid } from 'app/core/directives/valid';
import { Required } from 'app/core/directives/required';
```

### 24.2 Common Style Patterns
```scss
// Flexbox container
:host {
  display: flex;
  flex-direction: column;
  gap: vars.$spacing-md;
}

// Responsive grid
.grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: vars.$spacing-lg;
}

// Card container
.card {
  padding: vars.$spacing-lg;
  border-radius: vars.$border-radius;
  box-shadow: vars.$box-shadow;
  background: var(--bg);
}
```

---
## 25. Summary

This design system ensures:
- **Consistency**: Reusable components, tokens, patterns
- **Accessibility**: WCAG AA compliance, keyboard navigation, screen reader support
- **Performance**: Lazy loading, signal-based reactivity, minimal bundle size
- **Maintainability**: Clear conventions, documented patterns, minimal custom styles
- **Theming**: Light/dark mode support via CSS custom properties
- **Developer Experience**: Type-safe components, predictable APIs, comprehensive documentation

Follow these guidelines when creating new UI features, integrating Figma designs, or refactoring existing components. When in doubt, refer to existing implementations in `src/app/shared/ui/` and the permissions feature example.

---
**Document Version**: 1.0  
**Last Updated**: 2025-11-29  
**Maintainers**: Frontend Architecture Team

End of UI Design System Guidelines.
