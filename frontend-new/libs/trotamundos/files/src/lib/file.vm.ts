export class FileVM {
  id: string = null!;
  name: string = null!;
  description: string = null!;
  constructor(init?: Partial<FileVM>) { Object.assign(this, init); }
}
