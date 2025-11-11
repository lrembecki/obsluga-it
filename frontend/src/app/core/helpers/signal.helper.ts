/* eslint-disable no-unused-vars */
import { computed, Signal, signal, WritableSignal } from "@angular/core";

// Utility: shallow equality check used for diff extraction
function _shallowEqual(a: any, b: any): boolean {
    if (a === b) return true;
    if (typeof a !== 'object' || typeof b !== 'object' || a === null || b === null) return false;
    const aKeys = Object.keys(a);
    const bKeys = Object.keys(b);
    if (aKeys.length !== bKeys.length) return false;
    for (const k of aKeys) if (a[k] !== b[k]) return false;
    return true;
}

export class ContextModel<T> {

    public readonly computed: Signal<T> = null!;
    public readonly session: Signal<T> = null!;
    public readonly cache: WritableSignal<T | null> = null!

    constructor(
        existing: () => T,
        private readonly _onUpdate: (input: T) => T = (input) => input
    ) {
        this.computed = computed(existing);
        this.session = computed(() => this.cache() ?? this.computed());
        this.cache = signal<T | null>(null);
    };
    // Tracks whether hydrate() has been invoked
    private _hydrated = false;

    /**
     * Update the mutable session state. Provide an input to replace the session entirely
     * or omit to re-run the onUpdate transformer against current session value.
     */
    public update: (input?: T) => void = (input) => {
        this.cache.set(this._onUpdate(input ?? this.session()));
    };

    /**
     * Reset discards any dirty session edits and reverts to the pristine computed value.
     * Also clears the hydrated flag so future hydrate() calls may run again if needed.
     */
    public reset(): void {
        this.cache.set(null);
        this._hydrated = false;
    }

    /**
     * Lazily hydrate the session value (e.g., resolve ID-only placeholders to rich objects)
     * only once per reset cycle. Subsequent calls are ignored until reset().
     * Useful for virtual scroll scenarios: call hydrate() when rows become visible.
     */
    public hydrate(transform: (value: T) => T): void {
        if (this._hydrated) return;
        const base = this.session(); // session resolves to computed if cache empty
        this.cache.set(transform(base));
        this._hydrated = true;
    }

    /**
     * Produce a minimal shallow diff between the pristine computed value and current session.
     * Returns null when there are no changes. For primitive T, returns an object with 'previous'/'current'.
     * For object T, returns a map of changed keys to { previous, current } pairs.
     */
    public diff(): Record<string, { previous: any; current: any }> | null {
        const dirty = this.cache();
        if (dirty === null) return null; // no edits
        const original = this.computed();
        // Primitive or non-object case
        if (typeof original !== 'object' || original === null || typeof dirty !== 'object' || dirty === null) {
            return original === dirty ? null : { value: { previous: original, current: dirty } };
        }
        const result: Record<string, { previous: any; current: any }> = {};
        const keys = new Set<string>([...Object.keys(original), ...Object.keys(dirty)]);
        for (const k of keys) {
            const prev = (original as any)[k];
            const curr = (dirty as any)[k];
            if (!_shallowEqual(prev, curr)) {
                result[k] = { previous: prev, current: curr };
            }
        }
        return Object.keys(result).length ? result : null;
    }
}

export function cachedComputed<T>(
    existing: () => T,
    onUpdate: (input: T) => T = (input) => input
): ContextModel<T> {
    return new ContextModel<T>(
        existing,
        onUpdate
    );
}