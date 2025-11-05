export class TripVM {
    id: string = null!;
    title: string = null!;
    subtitle: string = null!;
    description: string = null!;

    constructor(init?: Partial<TripVM>) {
        Object.assign(this, init);
    }
}
