# obsluga-it Nx Workspace

Angular 20+ monorepo following architectural guardrails.

## Architecture

- **Angular 20+** with standalone components
- **Nx 22.x** monorepo
- **Signals-first**: `input()`, `output()`, `signal()`, `rxResource()`
- **strictTemplates** enabled

## Project Structure

```
apps/web          - Main Angular application
libs/core/auth    - Auth wrappers (MSAL, JWT, interceptor)
libs/core/http    - HTTP error normalization
libs/core/i18n    - i18n wrappers
libs/core/session-store     - Global session store
libs/core/app-config-store  - Global app config store
libs/shared/ui-kit  - PrimeNG wrappers
libs/shared/util    - Utility functions
libs/users/domain       - User business types
libs/users/data-access  - Users gateway
libs/users/feature      - Users page + store
libs/users/ui           - Users UI components
```

## Contribution Rules

1. No `@Input`/`@Output` - use `input()`/`output()` signals
2. Feature stores must use `rxResource` with Observable loaders
3. No raw `HttpErrorResponse` beyond `data-access` layer
4. No direct `primeng`, `@azure/msal-*`, `@ngx-translate/*` imports outside designated wrapper libs
5. Authentication: MSAL token only to `api-endpoint/account/authenticate`; internal JWT to all other APIs
6. All `localStorage` access must be browser-guarded (`isPlatformBrowser`)

## Running

```bash
# Lint all
npx nx run-many -t lint --all

# Test all
npx nx run-many -t test --all

# Build app
npx nx build web
```
