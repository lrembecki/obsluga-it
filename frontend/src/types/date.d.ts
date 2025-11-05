export { };

declare global {
    interface DateConstructor {
        /**
         * Converts a UTC date-only or midnight-UTC ISO string into a local Date that preserves
         * the intended calendar day in the user's timezone.
         *
         * Rules:
         * - If the input is a string in 'YYYY-MM-DD' (treated by JS as UTC midnight) -> shift by timezone offset.
         * - If the input is an ISO string ending with 'T00:00:00Z' (or .000Z) -> shift.
         * - If already a Date instance, we assume it has the correct local meaning and do not shift
         *   unless it represents midnight UTC EXACTLY and originated from a parse (hard to distinguish
         *   reliably, so we skip to avoid accidental double-adjust).
         *
         * This avoids off-by-one day issues when binding to date pickers or formatting with the date pipe.
         *
         * @param value Date | string
         * @returns Date (localized calendar date)
         */
        // eslint-disable-next-line no-unused-vars
        normalizeUtcDateToLocalCalendar(value: Date | string | null | undefined): Date | null;
    }
}