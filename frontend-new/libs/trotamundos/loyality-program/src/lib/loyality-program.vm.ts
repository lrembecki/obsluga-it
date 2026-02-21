export class LoyalityProgramVM {
  id: string = null!;
  name: string = null!;
  title: string = null!;
  description: string = null!;
  constructor(init?: Partial<LoyalityProgramVM>) { Object.assign(this, init); }
}
