export class TripVM {
    id: string = null!;
    name: string = null!;
    isActive: boolean = false;
    isDisabled: boolean = false;
    title: string = null!;
    subtitle: string = null!;
    description: string = null!;

    constructor(init?: Partial<TripVM>) {
        Object.assign(this, init);
    }
}
