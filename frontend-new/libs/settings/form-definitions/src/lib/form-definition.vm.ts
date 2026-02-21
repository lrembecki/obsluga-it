export class FormDefinitionVM {
  id: string = null!;
  name: string = null!;
  description: string = null!;

  constructor(init?: Partial<FormDefinitionVM>) {
    Object.assign(this, init);
  }
}
