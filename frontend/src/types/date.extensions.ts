// Runtime implementation + type augmentation for Date.normalizeUtcDateToLocalCalendar.
// Import this file once at app bootstrap (e.g., in main.ts) to ensure the method is available.
// DECISION: Provide single canonical normalization for date-only UTC strings to avoid duplication (2025-11-05)

// We intentionally augment the global Date constructor.
// TODO(tickets/replace-tripvm-static): Migrate usages of TripVM.normalizeUtcDateToLocalCalendar to Date.normalizeUtcDateToLocalCalendar and remove the duplicate static method.

// Assign implementation (using any cast to avoid needing declaration duplication here).
Date.normalizeUtcDateToLocalCalendar = function (value: Date | string | null | undefined): Date | null {
  if (value == null) return value as any;

  if (typeof value === 'string') {
    // Assume already a local semantic day. Avoid double-adjust.
    value = new Date(value);
  }

  const offsetMinutes = value.getTimezoneOffset();
  // Shift backward by the offset so that formatted local date shows intended calendar day.
  return new Date(value.getTime() - offsetMinutes * 60000);
};
