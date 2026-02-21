export class WebsiteVM {
  id: string = null!;
  name: string = null!;
  url: string = null!;

  constructor(init?: Partial<WebsiteVM>) {
    Object.assign(this, init);
  }
}
