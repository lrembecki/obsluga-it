# Trotamundos Highlights Feature

Standalone lazy-loaded feature mirroring the existing `advantages` pattern.

## Files
- `routes.ts` – declares list route with list + form components.
- `highlight.vm.ts` – simple view model class with `id`, `title`, `icon`.
- `highlight.provider.ts` – facade + injection helpers hitting `trotamundos/highlights` API endpoint.
- `highlight-list.ts` – table listing highlights.
- `highlight-form.ts` – create/edit form with title & icon fields.

## Navigation
Parent module route: `/modules/trotamundos/highlights` with child `list`, `create`, `:id` handled by shared `listRoute` helper.

## Notes
Adjust translation keys (`Highlights`, `Title`, `Icon`) in `public/i18n/*.json` as needed.
