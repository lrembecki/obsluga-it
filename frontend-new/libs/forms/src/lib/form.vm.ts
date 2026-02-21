export class FormEntryVM {
  id: string = null!;
  formDefinitionId: string = null!;
  data: Record<string, unknown> = {};
  createdAt: string = null!;

  constructor(init?: Partial<FormEntryVM>) {
    Object.assign(this, init);
  }
}
