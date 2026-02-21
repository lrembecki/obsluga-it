export class TripVM {
  id: string = null!;
  name: string = null!;
  description: string = null!;
  startDate: string = null!;
  endDate: string = null!;
  constructor(init?: Partial<TripVM>) { Object.assign(this, init); }
}
