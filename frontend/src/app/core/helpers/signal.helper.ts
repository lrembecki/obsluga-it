/* eslint-disable no-unused-vars */
import { computed, Signal, signal, WritableSignal } from "@angular/core";

export function cachedComputed<T>(
    existing: () => T, 
    onUpdate: (input: T) => T = (input) => input
): {
    computed: Signal<T>;
    session: Signal<T>;
    cache: WritableSignal<T | null>;
    update: (input?: T) => void;
} {

    const computedSignal = computed(existing);

    const cache = signal<T | null>(null!);
    const session = computed(() => cache() ?? computedSignal());

    return {
        computed: computedSignal,
        session,
        cache,
        update: (input?: T): void => cache.set(onUpdate(input ?? session())),
    }
}