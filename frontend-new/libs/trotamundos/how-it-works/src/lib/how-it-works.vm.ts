export class HowItWorksVM {
  id: string = null!;
  title: string = null!;
  content: string = null!;
  constructor(init?: Partial<HowItWorksVM>) { Object.assign(this, init); }
}
