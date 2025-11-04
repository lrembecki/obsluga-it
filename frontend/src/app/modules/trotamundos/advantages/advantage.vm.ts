export class AdvantageVM {
    id: string = null!;
    title: string = null!;
    content: string = null!;

    constructor(init?: Partial<AdvantageVM>) {
        Object.assign(this, init);
    }
}