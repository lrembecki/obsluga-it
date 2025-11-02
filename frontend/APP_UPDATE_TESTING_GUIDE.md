# App Update Feature - Testing Guide

This document describes how to test the automatic app update functionality that has been implemented in the Angular project.

## What's Been Implemented

1. **AppUpdateService** - A service that automatically checks for app updates every 15 seconds
2. **Automatic Notifications** - Users are automatically notified when a new version is available
3. **Manual Update Check** - Users can manually check for updates via the navbar menu
4. **Immediate Reload** - After confirming an update, the app automatically reloads to the new version

## How the Update System Works

### Automatic Checks
- The app checks for updates every 15 seconds when running in production mode
- Additional checks are triggered when:
  - The user comes back online
  - The user switches back to the app tab (focus event)
  - The app becomes visible again (visibility change)

### User Notification
- When a new version is detected, a native browser confirm dialog appears
- The dialog shows:
  - Title: "New Version Available" / "Dostępna nowa wersja"
  - Message: "A new version of the application is available. Click OK to update now."
  - OK button to confirm the update

### Update Process
1. User clicks OK in the confirmation dialog
2. Service worker activates the new version
3. Browser cache is cleared
4. Page automatically reloads with the new version

## How to Test

### Prerequisites
- The app must be built for production (`npm run build`)
- The app must be served from a web server (not file:// protocol)
- Service Worker must be enabled (only works in production builds)

### Testing Steps

1. **Initial Setup**
   ```bash
   npm run build
   npx serve dist/angular-project
   ```

2. **Make Changes**
   - Modify any file in the app (e.g., change a text in a component)
   - Build the app again: `npm run build`
   - Deploy the new build to the same server location

3. **Observe Automatic Update**
   - The app should detect the new version within 15 seconds
   - A confirmation dialog should appear automatically
   - Click OK to update

4. **Manual Update Check**
   - Use the "Check for Updates" option in the navbar menu
   - If no updates are available, you'll see a success notification
   - If updates are available, you'll get the confirmation dialog

## Service Worker Configuration

The service worker is configured with:
- `updateMode: "prefetch"` for the main app bundle
- Aggressive caching strategy for immediate update detection
- Proper cache invalidation on updates

## Translation Support

The update notifications support both English and Polish translations:

### English
- "New Version Available"
- "A new version of the application is available. Click OK to update now."
- "Check for Updates"
- "No Updates Available"

### Polish
- "Dostępna nowa wersja"
- "Dostępna jest nowa wersja aplikacji. Kliknij OK, aby zaktualizować teraz."
- "Sprawdź aktualizacje"
- "Brak dostępnych aktualizacji"

## Technical Implementation

### Key Files Modified
- `src/app/core/services/app-update.service.ts` - Main update service
- `src/app/app.config.ts` - Service registration
- `src/app/app.ts` - Service initialization
- `src/app/shared/ui/navbar/navbar.component.ts` - Manual update check
- `public/i18n/en.json` & `public/i18n/pl.json` - Translation keys
- `ngsw-config.json` - Service worker configuration

### Service Features
- Automatic initialization when the app starts
- 15-second interval checks for updates
- Event-driven checks (online, focus, visibility)
- Native browser confirm dialogs for immediate user attention
- Automatic cache clearing before reload
- Error handling for failed update checks
- Manual update check API for user-initiated checks

## Development Notes

- The update service only works in production builds (when `isDevMode()` returns false)
- Service Worker must be enabled and registered
- The confirmation dialog uses native `window.confirm()` for immediate user attention
- Cache clearing ensures the new version loads properly after reload
