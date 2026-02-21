export class HighlightVM {
  id: string = null!;
  title: string = null!;
  icon: string = null!;
  constructor(init?: Partial<HighlightVM>) { Object.assign(this, init); }
}
