export class EmailVM {
  id: string = null!;
  isActive: boolean = false;
  smtpServer: string = '';
  smtpPort: number = 0;
  smtpUsername: string = '';
  smtpPassword: string = '';
  fromAddress: string = '';
  fromName: string = '';
  replyToAddress: string = '';
  replyToName: string = '';
  constructor(init?: Partial<EmailVM>) {
    Object.assign(this, init);
  }
}
