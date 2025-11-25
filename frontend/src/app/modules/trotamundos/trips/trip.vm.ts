import { StorageVM } from "app/shared/storage/storage.vm";

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
    advantages: string[] = [];
    agenda: { order: number; content: string }[] = [];
    highlights: { order: number; highlightId: string; value: string }[] = [];
    images: { order: number; imageId: string; image: StorageVM }[] = [];

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
