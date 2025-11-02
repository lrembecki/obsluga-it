# Auto Remove Unused Imports Configuration

This document outlines the configuration changes made to automatically remove unused imports on save in the Angular project.

## Files Modified

### 1. VS Code Settings (`.vscode/settings.json`)
- Added `source.organizeImports` and `source.removeUnusedImports` to `codeActionsOnSave`
- Configured TypeScript preferences for import organization
- Added ESLint auto-fix on save

### 2. Workspace Settings (`../.vscode/settings.json`)
- Created workspace-level settings to ensure consistent behavior
- Configured ESLint working directories
- Set up file associations and validation

### 3. ESLint Configuration (`eslint.config.js`)
- Updated to ESLint 9 configuration format
- Added `eslint-plugin-unused-imports` plugin
- Configured rules to detect and auto-fix unused imports
- Set up proper TypeScript parsing

### 4. Package.json
- Added `type: "module"` for ES module support
- Added lint scripts:
  - `npm run lint` - Check for issues
  - `npm run lint:fix` - Auto-fix issues including unused imports

### 5. TypeScript Configuration (`tsconfig.json`)
- Added `noUnusedLocals` and `noUnusedParameters` compiler options
- Enhanced strict mode for better code quality

### 6. VS Code Tasks (`.vscode/tasks.json`)
- Added lint and lint:fix tasks
- Configured problem matchers for ESLint

## Dependencies Added
- `@typescript-eslint/eslint-plugin`
- `@typescript-eslint/parser`
- `@eslint/js`

## How It Works

### Automatic (On Save)
When you save a file in VS Code, the following actions are automatically triggered:
1. ESLint fixes all auto-fixable issues (including unused imports)
2. TypeScript organizes imports alphabetically
3. Removes any unused import statements

### Manual
You can also run the linter manually:
```bash
npm run lint        # Check for issues
npm run lint:fix    # Fix issues automatically
```

## Current Status
- ✅ Unused imports are detected and flagged as errors
- ✅ Auto-fix removes unused imports when saving files
- ✅ Import organization is configured
- ✅ TypeScript compilation flags unused variables
- ✅ ESLint rules are properly configured

## Example
Before save:
```typescript
import { JsonPipe } from '@angular/common';
import { Component, inject, Injectable, signal } from '@angular/core';
import { FileFacade } from 'app/core/facades/file.facade';  // ← Unused
import { Facade } from 'app/core/interfaces/facade.interface';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { environment } from 'environments/environment';
import { FileUpload, UploadEvent } from 'primeng/fileupload';
import { Subject } from 'rxjs';  // ← Unused
```

After save:
```typescript
import { JsonPipe } from '@angular/common';
import { Component, inject, Injectable, signal } from '@angular/core';
import { Facade } from 'app/core/interfaces/facade.interface';
import { TranslatePipe } from 'app/core/pipes/translate.pipe';
import { environment } from 'environments/environment';
import { FileUpload, UploadEvent } from 'primeng/fileupload';
```

## Benefits
- 🧹 Cleaner, more maintainable code
- 📦 Smaller bundle sizes (tree-shaking works better)
- 🚀 Improved development experience
- 🔍 Better code quality enforcement
- ⚡ Automatic cleanup on every save
