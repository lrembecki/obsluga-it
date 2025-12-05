Title: Trip viewmodel (frontend spec)
===================================

Summary
-------
This document describes the backend `TripVM` viewmodel and its nested viewmodels so the Angular frontend can model, validate and render trips consistently.

Backend references
- Main record: `lrembecki.core.trotamundos.ViewModels.TripVM` (`src\trotamundos\lrembecki.core.trotamundos\ViewModels\TripVM.cs`)
- Nested records: `TripAgendaVM`, `TripHighlightVM`, `TripImageVM`, `TripPaymentScheduleVM`, `TripPriceIncludeVM`, `TripRequirementVM`, `TripScheduleVM`, `TripSuggestedFlightVM` (see individual files under `src\trotamundos\lrembecki.core.trotamundos\ViewModels\*`).
- Entities used to build the VM live under `src\trotamundos\lrembecki.core.trotamundos\Entitites\*`.

Key mapping behavior
- The backend exposes `TripVM.Map(TripEntity)` which produces the viewmodel. Collections are mapped with `.Select(...Map)`.
- `Advantages` is an array of GUIDs extracted from `AdvantageEntity.Id` on the backend.
- `TripImageVM` includes a `StorageVM` (image metadata) via `StorageVM.Map(entity.Image)`. Mirror `StorageVM` structure on frontend if available.

TypeScript interfaces (suggested)
- Use these interfaces in the Angular app (adjust `StorageVM` to match backend definition if different).

```typescript
export type Guid = string; // UUID string

export interface TripHighlightVM {
  order: number;
  highlightId: Guid;
  value: string;
}

export interface TripImageVM {
  tripId: Guid;
  imageId: Guid;
  image: any; // replace `any` with real StorageVM interface when available
  order: number;
}

export interface TripPaymentScheduleVM {
  tripId: Guid;
  order: number;
  title: string;
  price: string;
  description: string;
}

export interface TripPriceIncludeVM {
  tripId: Guid;
  order: number;
  includes: boolean;
  content: string;
}

export interface TripRequirementVM {
  tripId: Guid;
  order: number;
  description: string;
}

export interface TripScheduleVM {
  tripId: Guid;
  order: number;
  title: string;
  content: string;
}

export interface TripSuggestedFlightVM {
  tripId: Guid;
  imageId: Guid;
  order: number;
}

export interface TripAgendaVM {
  // define according to backend TripAgendaVM (not shown here).
  // Typical fields: order: number; title?: string; content?: string;
  order: number;
  [key: string]: any;
}

export interface TripVM {
  id: Guid;
  name?: string | null;
  isActive: boolean;
  isDisabled: boolean;
  startDate?: string | null; // ISO 8601 date-time or null
  endDate?: string | null;   // ISO 8601 date-time or null
  calendar?: string | null;  // small string (calendar name/id)
  title: string;
  subtitle: string;
  description: string;
  agenda: TripAgendaVM[];
  advantages: Guid[]; // array of advantage ids
  highlights: TripHighlightVM[];
  images: TripImageVM[];
  paymentSchedules: TripPaymentScheduleVM[];
  priceIncludes: TripPriceIncludeVM[];
  requirements: TripRequirementVM[];
  schedules: TripScheduleVM[];
  suggestedFlights: TripSuggestedFlightVM[];
}
```

Field semantics and frontend guidance
- `id` — trip identifier (GUID string).
- `name` — optional user-friendly name.
- `isActive` / `isDisabled` — booleans controlling visibility/status.
- `startDate`, `endDate` — nullable; backend uses `DateTime?`. Expect ISO strings or `null`. Use locale formatting on display; handle `null`.
- `calendar` — optional short string (calendar id/name). Backend comment: "max length 50" — enforce similar limit in forms.
- `title`, `subtitle`, `description` — textual content for display.
- `agenda` — ordered items; backend updates by `Order`.
- `advantages` — array of `Guid` (IDs only) — frontend may fetch advantage details separately if needed.
- Collections (highlights, images, etc.) preserve `order` fields — sort by `order` if necessary for rendering.
- `images[*].image` — storage metadata; expected to contain URL and metadata. Replace `any` with backend `StorageVM` contract.

Sample JSON (what frontend may receive)
```json
{
  "id":"d6a9f0c2-...-1234",
  "name":"Amazon Adventure",
  "isActive": true,
  "isDisabled": false,
  "startDate":"2026-03-01T00:00:00Z",
  "endDate":"2026-03-10T00:00:00Z",
  "calendar":"2026-season-a",
  "title":"Amazon Adventure 2026",
  "subtitle":"Explore the rainforest",
  "description":"Full description text...",
  "agenda":[ { "order":1, "title":"Day 1", "content":"..." } ],
  "advantages":[ "9f8e7d...-a1b2" ],
  "highlights":[ { "order":1, "highlightId":"...","value":"Highlight text" } ],
  "images":[ { "tripId":"...", "imageId":"...", "image": { "url":"https://..." }, "order":1 } ],
  "paymentSchedules":[ { "tripId":"...", "order":1, "title":"Deposit","price":"100.00","description":"..." } ],
  "priceIncludes":[ { "tripId":"...", "order":1, "includes":true, "content":"Meals" } ],
  "requirements":[ { "tripId":"...", "order":1, "description":"Passport valid 6 months" } ],
  "schedules":[ { "tripId":"...", "order":1, "title":"Arrival", "content":"..." } ],
  "suggestedFlights":[ { "tripId":"...", "imageId":"...", "order":1 } ]
}
```

Frontend implementation notes
- Date handling: treat `startDate`/`endDate` as optional ISO strings; convert with `Date` or `date-fns`/`Luxon`.
- Sorting: rely on `order` where present; do `array.sort((a,b) => a.order - b.order)` before display.
- Validation: mirror backend constraints (e.g., `calendar` max length ~50, required `title`, etc.) in the form layer.
- Absent lists: expect arrays (empty arrays) rather than `null` for collections in `TripVM`. Defensive code should handle both.
- Advantage details: `advantages` contains IDs only — fetch advantage lookup separately if the UI needs label/text.
- Images: ensure `image` object contains a valid URL before rendering — provide fallback placeholder.

Where to get exact nested contracts
- For exact structures (e.g., `TripAgendaVM`, `StorageVM`) open `src\trotamundos\lrembecki.core.trotamundos\ViewModels\*` or ask backend team for the canonical JSON schema. Mirror those fields in TypeScript for type safety.

Example Angular usage pattern
- Create a service model and DTO mapping that receives backend JSON and returns typed `TripVM`.
- Use interfaces above (or generated models) to type components and forms.

If you want, I can:
- generate exact TypeScript interfaces for all nested VM files found in `src\trotamundos\lrembecki.core.trotamundos\ViewModels\` (requires those files), or
- produce an Angular service example that fetches and maps the trip to these interfaces.

GitHub Copilot