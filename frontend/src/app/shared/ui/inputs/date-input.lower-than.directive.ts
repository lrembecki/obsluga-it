import { Directive, effect, inject, input } from '@angular/core';
import { Valid } from 'app/core/directives/valid';
import { DateInputComponent } from './date-input.component';

@Directive({
    selector: 'app-date-input[lowerThan]'
})
export class DateInputLowerThanDirective {
    public readonly lowerThan = input.required<DateInputComponent | null>();
    private readonly _input = inject(DateInputComponent, { self: true });
    private readonly _valid = inject(Valid);

    constructor() {
        effect(() => {
            const compareTo = this.lowerThan();
            const current = this._input.value();
            const isValid =
                !compareTo ||
                !current ||
                current.getTime() < compareTo.value()!.getTime();
            this._valid.valid.set(isValid);
        })
    }
}