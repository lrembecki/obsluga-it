export class AboutUsVM {
  id: string = null!;
  title: string = null!;
  content: string = null!;
  constructor(init?: Partial<AboutUsVM>) { Object.assign(this, init); }
}
