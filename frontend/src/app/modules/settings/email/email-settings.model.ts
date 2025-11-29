export class EmailSettingsModel {
  smtpServer: string = null!;
  smtpPort!: number;
  smtpUsername: string = null!;
  smtpPassword: string = null!;
  useSsl: boolean = false;
  fromAddress: string = null!;
  fromName: string = null!;
  isDefault!: boolean;
  isActive!: boolean;
  replyToAddress?: string;
  replyToName?: string;

  constructor(init?: Partial<EmailSettingsModel>) {
    Object.assign(this, init);
  }
}
