export class EmailTemplateVM {
  id: string = null!;
  name: string = null!;
  subject: string = null!;
  body: string = null!;

  constructor(init?: Partial<EmailTemplateVM>) {
    Object.assign(this, init);
  }
}
