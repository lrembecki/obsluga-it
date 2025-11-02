export class ThemeModel {
  name: string = null!;

  constructor(init?: Partial<ThemeModel>) {
    Object.assign(this, init);
  }
}
