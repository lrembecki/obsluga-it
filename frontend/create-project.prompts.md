Create a new Angular project with the following specifications:

## 🔧 Technical Specifications

### Technology Versions
- **Angular CLI**: Latest LTS version (20.x)
- **Angular Framework**: Latest LTS version (20.x)
- **Node.js**: Latest version compatible with Angular LTS (20.x or higher)
- **PrimeNG**: Latest version compatible with Angular 20
- **TypeScript**: Version compatible with Angular 20
- **Package Manager**: npm
- **Target Browsers**: Chrome, Firefox
- **State Management**: Angular Signals (built-in reactive primitives)
  
### Build Configuration
- **Bundler**: webpack
- **Strict Mode**: Enabled for both Angular and TypeScript
- **Source Maps**: Enabled for development, disabled for production
- **Tree Shaking**: Enabled for production builds
- **Code Splitting**: Lazy loading modules and components

## Project Setup
- Create a new Angular project using Angular CLI with strict mode enabled
- Configure the project with strict TypeScript configuration
- Set up modern Angular features (standalone components where appropriate)

## UI Framework - PrimeNG
- Install and configure PrimeNG as the primary UI component library
- Set up PrimeNG theme (use the default Lara theme)
- Implement possibility to switch between dark/light/system specific themes
- Configure PrimeNG modules for common components (Button, Input, Dialog, Table, etc.)
- Import PrimeNG CSS and theme files in styles.css
- **Use Material Icons for iconography** (add `material-icons` font to project)
- Integrate PrimeNG form components with Angular Reactive Forms (FormControl, FormGroup)
- Create custom form input components that wrap PrimeNG components with reactive forms support

## Reactive Forms Strategy
- **Mandatory Usage**: All forms must use Angular Reactive Forms (FormControl, FormGroup, FormArray)
- **No Template-Driven Forms**: Template-driven forms are prohibited throughout the application
- **Form Validation**: Implement comprehensive validation using validators and custom validation functions
- **Form State Management**: Use Angular Signals to manage form state reactively
- **Custom Form Controls**: Create reusable form control components that implement ControlValueAccessor
- **Form Builders**: Use FormBuilder service for complex form construction
- **Dynamic Forms**: Support dynamic form generation based on configuration objects

## Internationalization - ngx-translate
- Install and configure ngx-translate for multi-language support
- Implement two languages:
  - Polish (pl)
  - English (en) - set as default language
- Create translation files in assets/i18n/:
  - en.json (English translations)
  - pl.json (Polish translations)
- Implement language switcher component with dropdown
- Store user's language preference in localStorage
- Load saved language preference on application startup
- Configure TranslateModule in app.module.ts with proper loader
- Create custom `TranslationService` in `core/services` folder that wraps ngx-translate functionality:
  - `translate(key: string, params?: any): Observable<string>` - Returns observable with translated text for reactive operations
  - `instant(key: string, params?: any): string` - Returns synchronous translated text for immediate use in components and services
  - Integration with Angular Signals for reactive translation state management

## Proxy Configuration
- Create proxy configuration to mask all assets and API requests behind one URL
- For assets use `/share` URL path
- For API use `/api` URL path
- Configure proxy.conf.json for development server

## Authentication - MSAL Angular for Azure B2C
Configure MSAL Angular with the following B2C settings:
```typescript
const msalConfig = {
  auth: {
    clientId: "ea39066f-89ff-417f-a66c-35b958b2b3cd",
    authority: "https://platformproduction.b2clogin.com/platformproduction.onmicrosoft.com/B2C_1_susi",
    redirectUri: window.location.origin,
    postLogoutRedirectUri: window.location.origin
  },
  cache: {
    cacheLocation: "localStorage",
    storeAuthStateInCookie: false
  }
};

const protectedResourceMap = new Map([
  ['https://graph.microsoft.com/v1.0/me', ['user.read']],
  [`https://localhost:7138/api/account, ['access_as_user']]
]);

const msalGuardConfig = {
  interactionType: InteractionType.Redirect, // Ensure redirection is used for authentication
  authRequest: {
    scopes: ['access_as_user']
  }
};
```
**Note:** MSAL authentication must use `InteractionType.Redirect` for login and token acquisition flows.

Additional B2C configuration:
- Instance: "https://platformproduction.b2clogin.com/"
- Domain: "platformproduction.onmicrosoft.com"
- TenantId: "7c459008-9bba-4390-86cc-ef6fecd78d63"
- CallbackPath: "/signin-oidc"
- SignUpSignInPolicyId: "B2C_1_susi"
- SignedOutCallbackPath: "/signout/B2C_1_susi"
- ResetPasswordPolicyId: "b2c_1_reset"
- EditProfilePolicyId: "b2c_1_edit_profile"

### Authentication Features
- Automatic token refresh
- Silent token acquisition
- Logout handling with proper cleanup
- Role-based access control
- Auth error handling and user feedback

## Authentication & User Session Flow

### User Token & Data Initialization

On website entry, implement the following authentication and user session logic:

1. **Check for `user-token` in localStorage**  
   - If `user-token` does **not** exist:
     - Call the `GET` method on `https://localhost:7138/api/account` (requires MSAL authentication).
     - The API returns a bearer token and user data in the format:
       ```json
       {
         "token": "string",
         "account": {
           "email": "string",
           "permissions": ["string"],
           "subscriptionId": "string"
         }
       }
       ```
     - Store the received token in localStorage as `user-token`.
     - Store the user information in localStorage as `user-data`.

2. **If `user-token` exists:**  
   - Use the stored token and user data from localStorage.

### Permissions Management

- User permissions must be read from `user-data.permissions` in localStorage.
- All permission checks and role-based access control throughout the application should reference `user-data.permissions`.
- Update the permission union type in `core/defaults/permission.default.ts` as new features or sub-features are added.

### Subscription Management

- The user's current subscription is defined by `user-data.subscriptionId` in localStorage.
- All subscription-specific logic should reference `user-data.subscriptionId`.

## Folder Architecture

Use a domain-driven, feature-based structure. Example:

```
src/
├── app/
│   ├── core/         // domain services, models, providers, shared utilities, defaults
│   ├── shared/       // reusable UI components, inputs, directives
│   ├── layout/       // layout components (header, sidebar, footer, etc.)
│   ├── features/     // business features, grouped by domain
│   ├── types/        // global type extensions
├── assets/
│   ├── i18n/         // translation files
│   ├── images/
│   └── styles/
└── environments/
    ├── environment.ts
    ├── environment.prod.ts
    └── environment.staging.ts
```

## Development Tools & Quality

### Code Quality Tools
- **ESLint**: Configure with Angular-specific rules and strict settings
- **Prettier**: Set up for consistent code formatting

### Testing Configuration
- **Unit Testing**: Jasmine/Karma with coverage reports (minimum 80%)
- **E2E Testing**: Consider Cypress or Playwright setup
- **Test Utilities**: Angular Testing Library for better testing practices

## Environment Configuration

Create environment files with the following structure:
```typescript
export const environment = {
  production: false,
  apiUrl: '/api',
  assetsUrl: '/share',
  msalConfig: {
    // MSAL configuration
  },
  features: {
    enableThemeSwitcher: true,
    enableAnalytics: false
  }
};
```

## Performance & Optimization

### Bundle Optimization
- Implement OnPush change detection strategy where possible
- Use trackBy functions in *ngFor loops
- Lazy load feature modules and routes
- Implement image optimization strategies
- Leverage Angular Signals for reactive state management with automatic change detection optimization

### State Management with Angular Signals
- Use Angular Signals for reactive state management
- Implement computed signals for derived state
- Use effect() for side effects and reactive operations
- Create signal-based services for shared state across components
- Leverage signal inputs and outputs for component communication

### Caching Strategy
- HTTP response caching with appropriate headers
- Browser storage for user preferences
- Service worker for offline capabilities (optional)
- Signal-based caching for computed values

## Additional Requirements

1. **Standalone Components**: All components must be standalone components with explicit imports
2. **Provider Functions**: Use provider functions instead of modules for dependency injection configuration
3. **Lazy Loading**: Configure lazy loading for all feature routes using standalone components
4. **Routing**: 
   - Set up hierarchical routing with authentication guards using standalone route configuration
   - **Each feature folder must have its own `routes.ts` file**
   - **Lazy load children via routes, not components, for every feature**
5. **HTTP Interceptors**: Token injection, error handling, loading states
6. **TypeScript Configuration**: Strict mode with path mapping
7. **Responsive Design**: Mobile-first approach with PrimeNG responsive utilities
8. **Accessibility**: WCAG 2.1 AA compliance where possible
9. **Error Handling**: Global error handling with user-friendly messages
10. **State Management**: Angular Signals for reactive state management with automatic change detection optimization
11. **Model Organization**: Feature-specific models organized within each feature folder under models/ subfolder, with shared/common models in core/shared/models/
12. **Facade Organization**: Feature-specific facades organized within each feature folder under facades/ subfolder, with core domain facades organized by business domain
13. **Translation Service**: Custom TranslationService in core/translation/services with both synchronous (`instant`) and asynchronous (`translate`) methods, integrated with Angular Signals for reactive translations
14. **Translation Pipe**: Custom translate pipe in core/translation/pipes for simplified template usage, reducing boilerplate code
15. **Global Type Extensions**: Types folder with global extensions for built-in JavaScript objects (Date, String, Array, etc.)
16. **Component Architecture**: All components as standalone with proper imports, no NgModules
17. **Domain-Driven Design**: Core folder organized by business domains (auth, user, api, theme, translation, etc.) with proper aggregation
18. **Reactive Forms Only**: Mandatory use of Angular Reactive Forms (FormControl, FormGroup, FormArray) for all form implementations, no template-driven forms allowed
19. **Model-Based Two-Way Binding**: All input components must use `private readonly value = model()` for reactive value management and automatic FormControl synchronization via FormControlDirective
20. **Signal-Based Input/Output**: All components must use `input()` and `output()` signal functions instead of `@Input()` and `@Output()` decorators

## Domain-Driven Core Architecture

### Core Domain Organization Strategy
The core folder is organized by business domains, each containing:
- **Models**: Domain-specific interfaces and types
- **Services**: Business logic and state management for the domain
- **Guards/Interceptors**: Domain-specific cross-cutting concerns
- **Providers**: Dependency injection configuration for the domain

### Core Domain Examples

**Auth Domain (core/auth/):**
- Handles authentication, authorization, and security concerns
- Contains auth models, services, guards, and interceptors
- Manages authentication state and token handling

**User Domain (core/user/):**
- Manages user profile and preferences
- Contains user-related models and state management
- Provides user facade for complex operations

**API Domain (core/api/):**
- Handles HTTP communication and error handling
- Contains API response models and error handling logic
- Provides interceptors for global API concerns

**Theme Domain (core/theme/):**
- Manages application theming and styling
- Contains theme models and theme switching logic
- Provides theme state management

**Translation Domain (core/translation/):**
- Handles internationalization and localization
- Contains translation services and pipes
- Manages language switching and translation state

**Input Domain (shared/inputs/):**

### Domain Provider Examples
**core/auth/providers/auth.providers.ts:**
```typescript
import { Provider } from '@angular/core';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { MsalService, MsalGuard, MsalBroadcastService } from '@azure/msal-angular';
import { authInterceptor } from '../interceptors/auth.interceptor';

export const authProviders: Provider[] = [
  MsalService,
  MsalGuard,
  MsalBroadcastService,
  provideHttpClient(withInterceptors([authInterceptor]))
];
```

**core/translation/providers/translation.providers.ts:**
```typescript
import { Provider } from '@angular/core';
import { importProvidersFrom } from '@angular/core';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

export const translationProviders: Provider[] = [
  importProvidersFrom(
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader,
        deps: [HttpClient]
      }
    })
  )
];
```

### Updated Core Providers Configuration
**core/providers/core.providers.ts:**
```typescript
import { Provider } from '@angular/core';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { authProviders } from '../auth/providers/auth.providers';
import { translationProviders } from '../translation/providers/translation.providers';
import { themeProviders } from '../theme/providers/theme.providers';

export const coreProviders: Provider[] = [
  ...authProviders,
  ...translationProviders,
  ...themeProviders,
  // Reactive Forms are imported in components directly as standalone
];
```

## Host Control Directive

All custom input components must use a single directive for ControlValueAccessor implementation:

```typescript
@Directive({
  standalone: true,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: HostControlDirective,
    },
  ],
})
export class HostControlDirective implements ControlValueAccessor {

  control: FormControl;

  private injector = inject(Injector);
  private subscription?: Subscription;

  ngOnInit() {
    const ngControl = this.injector.get(NgControl, null, { self: true, optional: true });

    if (ngControl instanceof NgModel) {
      this.control = ngControl.control;
      this.subscription = ngControl.control.valueChanges.subscribe((value) => {
        if (ngControl.model !== value || ngControl.viewModel !== value) {
          ngControl.viewToModelUpdate(value);
        }
      });

    } else if (ngControl instanceof FormControlDirective) {
      this.control = ngControl.control;

    } else if (ngControl instanceof FormControlName) {
      const container = this.injector.get(ControlContainer).control as FormGroup;
      this.control = container.controls[ngControl.name] as FormControl;

    } else {
      this.control = new FormControl();
    }
  }

  writeValue() { }
  registerOnChange() { }
  registerOnTouched() { }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }
}
```

## Input Component Creation

The following input components must be created in the new project.  
**Each must use HostControlDirective via `hostDirectives: [HostControlDirective]` and bind `[formControl]="hcd.control"` in their template.** Additionally, all input components must wrap PrimeNG components for their respective input types.

- TextInput: Use PrimeNG's `<p-inputText>` component.
- NumberInput: Use PrimeNG's `<p-inputNumber>` component.
- DateInput: Use PrimeNG's `<p-calendar>` component with `date` mode.
- TimeInput: Use PrimeNG's `<p-calendar>` component with `time` mode.
- DropdownInput: Use PrimeNG's `<p-dropdown>` component.
- MultiSelectInput: Use PrimeNG's `<p-multiSelect>` component.
- CheckboxInput: Use PrimeNG's `<p-checkbox>` component.
- TextareaInput: Use PrimeNG's `<p-inputTextarea>` component.

**Example usage in a component:**
```typescript
@Component({
  standalone: true,
  imports: [ReactiveFormsModule, InputTextModule],
  selector: 'app-custom-input',
  template: `
    <p-inputText [formControl]="hcd.control"></p-inputText>
  `,
  hostDirectives: [HostControlDirective],
})
export class CustomInput {
  hcd = inject(HostControlDirective);
}
```

## Import Path Configuration

- All imports in the project must use **absolute paths**.
- The project must be configured so that the root path for imports starts from `src`.
- Example import:  
  ```typescript
  import { ApiService } from 'src/app/core/api/services/api.service';
  import { ExampleModel } from 'src/app/features/example/models/example.model';
  ```

- Update `tsconfig.json` and `tsconfig.app.json` to set `"baseUrl": "./src"` and configure `"paths"` as needed.

## Facade Pattern for Data Endpoints

For each data endpoint in every feature, create a generic facade class based on the feature's model.

**Model Requirements:**
- All models used for facades must implement the interface `IHasIdentity<T>` with an `identity: T` property.
- The `identity` property will be used for comparison and deduplication of records in the facade.

**Example IHasIdentity interface:**
```typescript
// filepath: src/app/core/shared/models/has-identity.model.ts
export interface IHasIdentity<T> {
  identity: T;
}
```

**Requirements:**
- Each feature must have its own facade in `facades/` using this pattern.
- Facades must only import from `core` and their own feature folder.
- All data access and synchronization logic should be encapsulated in the facade.
- **All services must be injected using the `inject()` function, declared as `private readonly` with a `_` prefix. Do not use constructor injection.**
- **All imports must use absolute paths starting from `src`.**
- **All models used for facades must implement `IHasIdentity<T>` and use the `identity` property for record comparison.**

---

### Angular Facade Pattern (with Signals, Absolute Imports, and Service Injection)

**Purpose:** Encapsulates data access and synchronization logic for a feature, combining API and cache sources.

**Model:** Uses a feature-specific model that implements `IHasIdentity<T>` (must have an `identity` property).

**Signals:**
- `_api`: Holds API data as a signal array.
- `_cache`: Holds cached data as a signal array.

**Service Injection:**
- Inject services using `inject()` (not constructor), declare as `private readonly _serviceName`.
- Injected services are optional

**Computed Data:**
- `data`: Combines cache and API data, deduplicates by `identity`.

**Populate Method:**
- Fetches data from API and cache if not null, updates signals.

**Imports:**
- Use absolute imports starting from `src`.

### Permission Defaults

Create a `permission.default.ts` file in `core/defaults` that exports a union type of all permissions.  
Each permission represents a feature or sub-feature using the format:

- `'[FeatureName]'` for features
- `'[FeatureName].[SubFeatureName]'` for sub-features

Example:
```typescript
export type permissions =
  'dashboard' |
  'user' |
  'user.profile' |
  'user.settings' |
  'reports' |
  'reports.analytics';
```
Update this file as you add new features or sub-features.