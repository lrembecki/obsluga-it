import { inject } from '@angular/core';
import { MessageService } from 'primeng/api';

export class NotificationService {
  private readonly _message = inject(MessageService);

  error(input: NotificationInput): void {
    this.message(input, 'error');
    console.error({ input });
  }
  success(input: NotificationInput): void {
    this.message(input, 'success');
    console.info({ input });
  }

  private message(input: NotificationInput, severity: string) {
    this._message.add({
      severity,
      summary: input.title,
      detail: input.message,
    });
  }
}

export class NotificationInput {
  public readonly title: string = null!;
  public readonly message: string = null!;

  constructor(init?: Partial<NotificationInput>) {
    Object.assign(this, init);
  }
}
