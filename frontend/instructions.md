# Development Instructions

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

### Template Directives
- **Control-Flow Directives**: Use Angular `control-flow` directives (`ngIf`, `ngFor`, etc.) instead of structured directives (`*ngIf`, `*ngFor`).
- **Reason**: Improves readability and aligns with modern Angular best practices.

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

- **Mandatory Usage**: All user-visible content (e.g., labels, tooltips, placeholders, button texts, etc.) must use the `translate` pipe in templates.
- **Custom Translation Pipe**: Use the custom `translate` pipe provided in `core/translation/pipes` for simplified template usage.
- **Validation**: Ensure all static text in templates is replaced with translation keys and uses the `translate` pipe.
- **Example**:
  ```html
  <button mat-button [matTooltip]="'TOOLTIP_KEY' | translate">
    {{ 'BUTTON_LABEL_KEY' | translate }}
  </button>
  ```

## Proxy Configuration
- Create proxy configuration to mask all assets and API requests behind one URL
- For assets use `/share` URL path
- For API use `/api` URL path
- Configure proxy.conf.json for development server

## Authentication - MSAL Angular for Azure B2C
Configure MSAL Angular with the following B2C settings:
```typescript
const msalConfig = {
  auth: {…}
};

const protectedResourceMap = new Map([
  ['https://graph.microsoft.com/v1.0/me', ['user.read']],
  [`https://localhost:7138/api/account, ['access_as_user']]
]);

const msalGuardConfig = {
  interactionType: InteractionType.Redirect, // Ensure redirection is used for authentication
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

2. **If `user-token` exists:**  
   - Use the stored token and user data from localStorage.

### Permissions Management

- User permissions must be read from `user-data.permissions` in localStorage.
- All permission checks and role-based access control throughout the application should reference `user-data.permissions`.
- Update the permission union type in `core/defaults/permission.default.ts` as new features or sub-features are added.

### Subscription Management

- The user's current subscription is defined by `user-data.subscriptionId` in localStorage.
- All subscription-specific logic should reference `user-data.subscriptionId`.

### Expiration

- The user's expiration date is defined by `user-data.expires` in localStorage
- Once expires date is lower then now `user-data` and `user-token` will be deleted from storage and signing-process will be exucuted.

## Folder Architecture

Use a domain-driven, feature-based structure. Example:

```
src/
├── app/
│   ├── core/         // defaults, interceptors, models, pipes, providers, services
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
    └── environment.staging.ts
```

### Core Folder Organization

The `core` folder is now organized by functionality rather than domain-driven aggregation. Each subfolder contains logic grouped by its functional purpose, such as services, guards, interceptors, and utilities.

**Updated Core Folder Examples:**

- **guards/**: Contains route guards for authentication and permissions
- **interceptors/**: Contains HTTP interceptors for token injection, error handling, etc.
- **services/**: Contains shared services used across the application
- **models/**: Contains shared models used across the application
- **providers/**: Contains dependency injection providers for core services

Ensure all new features or sub-features follow this functional structure for consistency.

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
  )
];
```

### Updated Core Providers Configuration
**core/providers/core.providers.ts:**
```typescript
import { Provider } from '@angular/core';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
Update this file as you add new features or sub-features.
```

# Translation Keys

Add the following keys to the translation files:

**en.json**
```json
{
  "FOOTER": {
    "ALL_RIGHTS_RESERVED": "All rights reserved.",
    "CHOOSE_LANGUAGE": "Choose Language"
  }
}
```

**pl.json**
```json
{
  "FOOTER": {
    "ALL_RIGHTS_RESERVED": "Wszelkie prawa zastrzeżone.",
    "CHOOSE_LANGUAGE": "Wybierz język"
  }
}
```

<h1>{{ 'PAGE_TITLE' | translate }}</h1>
<p>{{ 'DESCRIPTION_TEXT' | translate }}</p>
<input placeholder="{{ 'PLACEHOLDER_TEXT' | translate }}" />
