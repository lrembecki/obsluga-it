export class PriceVM {
  value: number = null!;
  currency: string = null!;
  description: string = null!;

  constructor(init?: Partial<PriceVM>) {
    Object.assign(this, init);
  }
}
