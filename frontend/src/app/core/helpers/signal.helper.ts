/* eslint-disable no-unused-vars */
import { computed, Signal, signal, WritableSignal } from "@angular/core";

class ContextModel<T> {

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
    public update: (input?: T) => void = (input) => {
        this.cache.set(this._onUpdate(input ?? this.session()));
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