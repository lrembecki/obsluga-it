export class TripVM {
    id: string = null!;
    name: string = null!;
    isActive: boolean = false;
    isDisabled: boolean = false;
    title: string = null!;
    subtitle: string = null!;
    description: string = null!;
    startDate: Date = null!;
    endDate: Date = null!;
    calendar: string | null = null;  // Up to 50 chars or null

    constructor(init?: Partial<TripVM>) {
        Object.assign(this, init);

        if (this.startDate) {
            this.startDate = Date.normalizeUtcDateToLocalCalendar(this.startDate)!;
        }
        if (this.endDate) {
            this.endDate = Date.normalizeUtcDateToLocalCalendar(this.endDate)!;
        }
    }
}
